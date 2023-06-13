//  Mou,Xiaohua 2023/04/03 16:56

using System.ComponentModel;

namespace HKSH.Common.CommonEnum
{
    /// <summary>
    /// TaskType
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// The device
        /// </summary>
        [Description("Device")] Device = 1001,

        /// <summary>
        /// The location
        /// </summary>
        [Description("Location")] Location = 1002,

        /// <summary>
        /// The role
        /// </summary>
        [Description("Role")] Role = 1003,

        /// <summary>
        /// The module
        /// </summary>
        [Description("Module")] Module = 1004,

        /// <summary>
        /// The menu
        /// </summary>
        [Description("Menu")] Menu = 1005,

        /// <summary>
        /// The dictionary setting
        /// </summary>
        [Description("DictionarySetting")] DictionarySetting = 1006,


        /// <summary>
        /// The dictionary data set
        /// </summary>
        [Description("DictionaryDataSet")] DictionaryDataSet = 1007,


        /// <summary>
        /// The dictionary
        /// </summary>
        [Description("Dictionary")] Dictionary = 1008,


        /// <summary>
        /// The alert
        /// </summary>
        [Description("Alert")] Alert = 1009,


        /// <summary>
        /// The button
        /// </summary>
        [Description("Button")] Button = 1010,
    }
}