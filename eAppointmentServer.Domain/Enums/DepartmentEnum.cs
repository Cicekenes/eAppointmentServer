using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Domain.Enums
{
    public sealed class DepartmentEnum : SmartEnum<DepartmentEnum>
    {
        public static readonly DepartmentEnum Acil = new DepartmentEnum("Acil",1);
        public static readonly DepartmentEnum Radyoloji = new DepartmentEnum("Radyoloji", 2);
        public static readonly DepartmentEnum Kardiyoloji = new DepartmentEnum("Kardiyoloji", 3);
        public static readonly DepartmentEnum Dermatoloji = new DepartmentEnum("Dermatoloji", 4);
        public static readonly DepartmentEnum Endokrinoloji = new DepartmentEnum("Endokrinoloji", 5);
        public static readonly DepartmentEnum Gastroenteroloji = new DepartmentEnum("Gastroenteroloji", 6);
        public static readonly DepartmentEnum GenelCerrahi = new DepartmentEnum("Genel Cerrahi", 7);
        public static readonly DepartmentEnum JinekolojiveObstetrik = new DepartmentEnum("Jinekoloji ve Obstetrik",8 );
        public static readonly DepartmentEnum Hematoloji = new DepartmentEnum("Hematoloji", 9);
        public static readonly DepartmentEnum EnfeksiyonHastaliklari = new DepartmentEnum("Enfeksiyon Hastalıkları", 10);
        public static readonly DepartmentEnum Nefroloji = new DepartmentEnum("Nefroloji", 11);
        public static readonly DepartmentEnum Noroloji = new DepartmentEnum("Nöroloji", 12);
        public static readonly DepartmentEnum Ortopedi = new DepartmentEnum("Ortopedi", 13);
        public static readonly DepartmentEnum Pediatri = new DepartmentEnum("Pediatri", 14);
        public static readonly DepartmentEnum Psikiyatri = new DepartmentEnum("Psikiyatri", 15);
        public static readonly DepartmentEnum Pulmonoloji  = new DepartmentEnum("Psikiyatri", 16);
        public DepartmentEnum(string name, int value) : base(name, value)
        {
        }
    }
}
