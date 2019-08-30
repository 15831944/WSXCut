using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel
{
    public class ArrayModel
    {
        private int rows;
        public int Rows
        {
            get
            {
                return this.rows;
            }
            set
            {
                if (value < 1)
                {
                    this.rows = 1;
                }
                this.rows = value;
            }
        }

        private int column;
        public int Column
        {
            get
            {
                return this.column;
            }
            set
            {
                if (value < 1)
                {
                    this.column = 1;
                }
                this.column = value;
            }
        }

        public double RowMargin { get; set; }
        public double ColumnMargin { get; set; }

        /// <summary>
        /// 阵列偏移类型，0：间距；1：偏移
        /// </summary>
        public int ArrayType { get; set; }

        /// <summary>
        /// 行生长方向，0：向上；1：向下
        /// </summary>
        public int RowDirection { get; set; }

        /// <summary>
        /// 列生长方向，0：向左；1：向右
        /// </summary>
        public int ColumnDirection { get; set; }
    }
}
