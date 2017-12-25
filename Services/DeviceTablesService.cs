using System;
using System.Collections.Generic;
using System.Linq;
using DivisionWebGlobal.Models.Structure;
using DivisionWebGlobal.Models.Data;
using DivisionWebGlobal.DAL;
using DivisionWebGlobal.Models.Dto;

// TODO: create new collections
namespace DivisionWebGlobal.Services
{
    public class DeviceTablesService
    {
        /// <summary>
        /// Запросить таблицы устройств DV-HEAD OMEGA 
        /// </summary>
        /// <param name="id">Идентификатор DV-HEAD</param>
        /// <returns></returns>
        public TablesDto RequestDeviceTables(int id)
        {
            TablesDto tablesDto = new TablesDto();
            try {
                using (MainDbContext dbContext = new MainDbContext()) {
                    HeadTable hd = dbContext.DvHeadTables.Where(t => t.Idhead == id).OrderByDescending(t => t.DgsTime).First();
                    tablesDto.DvHead = hd.DvHead;
                    tablesDto.ConfigurationTable = ParseConfigTable(hd.ConfigurationTable).OrderBy(s => s); // the original was not sorted
                    tablesDto.ExternalTable = ParseExternalTable(hd.ConnectedTable, tablesDto.ConfigurationTable);
                    tablesDto.Address = hd.DvHead.Address;
                    tablesDto.Failure = false;
                }
            } catch (Exception)
            {
                tablesDto.Failure = true;
            }
            return tablesDto;
        }

        /// <summary>
        /// Обработать данные с таблицей устройств по конфигурации. Подсветить красным аварийные устройства
        /// </summary>
        /// <param name="table">Конфигурационная таблица</param>
        private IEnumerable<ConfigurationDevice> ParseConfigTable(string table)
        {
            List<ConfigurationDevice> _tableConfig = new List<ConfigurationDevice>();
            table = table.Remove(0, 1);
            var cntrs = table.Split(':');

            foreach (String cntr in cntrs)
            {
                String type = cntr.Substring(1, 2);
                String address = cntr.Substring(5, 2);
                String port = cntr.Substring(9, 2);
                String stateAfterScan = cntr.Substring(13, 2);
                String fatalErrorFlag = cntr.Substring(17, 2);
                String errorCount = cntr.Substring(21, 4);
                String lastErrorDate = cntr.Substring(27, 10);
                String time = lastErrorDate.Substring(6, 2) + "." + lastErrorDate.Substring(8, 2) + " " + lastErrorDate.Substring(0, 2) + ":" + 
                    lastErrorDate.Substring(2, 2) + ":" + lastErrorDate.Substring(4, 2);

                ConfigurationDevice cd = new ConfigurationDevice(type, 
                                                                Convert.ToInt32(address, 16),
                                                                Convert.ToInt32(port, 16), 
                                                                stateAfterScan, 
                                                                fatalErrorFlag, 
                                                                Convert.ToInt32(errorCount, 16), 
                                                                time);
                cd.ReplaceErrorCodes();
                string typeEvident = cd.ExtractDeviceNameByCode();
                if (cd.Address == 0)
                {
                    if (typeEvident.Equals("DV-REG"))
                    {
                        cd.Type = "DV-HEAD (REG)";
                    }
                    if (typeEvident.Equals("DV-RB 30"))
                    {
                        cd.Type = "DV-HEAD (реле)";
                    }
                    if (typeEvident.Equals("30-входы"))
                    {
                        cd.Type = "DV-HEAD (" + typeEvident + ")";
                    }
                } else
                {
                    cd.Type = typeEvident;
                }

                //cd.Type = cd.Address == 0 ? "DV-HEAD (" + typeEvident + ")" : typeEvident;

                _tableConfig.Add(cd);
                // TODO: check in multithreading mode
            }
            return _tableConfig;
        }

        /// <summary>
        /// Обработать данные с таблицей внешних устройств
        /// </summary>
        /// <param name="table"></param>
        private IEnumerable<ExternalDevice> ParseExternalTable(string table, IEnumerable<ConfigurationDevice> configTable)
        {
            // формируем таблицу внешних устройств
            List<ExternalDevice> _tableExternal = new List<ExternalDevice>();
            table = table.Remove(0, 1);
            var cntrs = table.Split(':');

            // добавить в _tableExternal строки с названиями устройств на борту DV-HEAD
            foreach (ConfigurationDevice confDevice in configTable)
            {
                if (confDevice.Port == 0)
                {
                    _tableExternal.Add(confDevice.ConvertToExternalDevice());
                }
            }

            // добавить оставшиеся устройства
            foreach (String cntr in cntrs)
            {
                String port = cntr.Substring(1, 2);
                String type = cntr.Substring(5, 2);
                String address = cntr.Substring(9, 2);

                ExternalDevice cd = new ExternalDevice(type, Convert.ToInt32(address, 16), Convert.ToInt32(port, 16));
                cd.Type = cd.ExtractDeviceNameByCode();
                _tableExternal.Add(cd);
            }

            // TODO убрать лишние локальные переменные
            var _tableExternalSortedList = _tableExternal.OrderBy(s => s).ToList<ExternalDevice>();
            var _tableConfigSortedArray = configTable.ToArray();
            List<ExternalDevice> newExternalDevices = new List<ExternalDevice>();

            // убрать устройства, которых нет в ConfigurationTable
            foreach (ExternalDevice _externalDevice in _tableExternalSortedList)
            {
                bool flag = true;
                foreach (ConfigurationDevice _configDevice in _tableConfigSortedArray)
                {
                    if (_externalDevice.Equals(_configDevice))
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    _tableExternal.Remove(_externalDevice);
                    newExternalDevices.Add(_externalDevice);
                }
            }

            // отсортировать таблицу устройств ExternalDevice добавив пустые строки там, где устройств нет. 
            _tableExternal = _tableExternal.OrderBy(s => s).ToList<ExternalDevice>();
            // добавим пустые строки
            for (int i = 0; i < _tableConfigSortedArray.Length; i++ )
            {
                ConfigurationDevice configDev = _tableConfigSortedArray[i];
                bool flag = true;
                // TODO Implement Collection for ExternalDevices: configDev.Find()

                // пока сравнить вручную
                foreach (ExternalDevice _externalDevice in _tableExternalSortedList)
                {
                    if (_externalDevice.Equals(configDev))
                    {
                        flag = false;
                        break;
                    } 
                }

                // добавить пустую линию TODO проверить на клиенте, перепрошив DV-HEAD TODO добавить тест. На клиенте заменить отрицательные значения
                if (flag)
                {
                    // TODO реализовать отдельные классы - 1) контейнер устройств 2) внешний modelview для передачи наружу с пустыми строками
                    ExternalDevice nullDevice = new ExternalDevice("ttttt", -20, -20); // TODO заменить статическим экземпляром
                    nullDevice.NullType = true;
                    _tableExternal.Insert(i, nullDevice);
                }
            }


            // Все лишние устройства расположить в хвостовой части таблицы
            foreach (ExternalDevice newExternalDevice in newExternalDevices)
            {
                _tableExternal.Add(newExternalDevice);
            }

            return _tableExternal;
        }

    }
}