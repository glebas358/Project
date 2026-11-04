using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Enums
{
    public enum UserRole
    {
        /// <summary>
        /// Сотрудник пункта выдачи.
        /// Может принимать посылки, искать их, выдавать и оформлять возврат.
        /// </summary>
        Employee = 1,

        /// <summary>
        /// Руководитель пункта выдачи.
        /// Имеет доступ ко всем функциям сотрудника, а также к сводкам,
        /// журналу операций и списку возвратов.
        /// </summary>
        Manager = 2
    }
}
