using System;
using System.Collections.Generic;
using System.Text;

#nullable disable

namespace DLNLTT
{
    public partial class SoLieu
    {
        private object utf8;

        public int Id { get; set; }
        public string MtHt { get; set; }
        public string MtCsln { get; set; }
        public string MtTk { get; set; }
        public string MtSln { get; set; }
        public string GHt { get; set; }
        public string GCsln { get; set; }
        public string GTk { get; set; }
        public string GSln { get; set; }
        public string SkHt { get; set; }
        public string SkCsln { get; set; }
        public string SkTk { get; set; }
        public string SkSln { get; set; }
        public string THt { get; set; }
        public string TCsln { get; set; }
        public string TTk { get; set; }
        public string TSln { get; set; }
        public string ThoiGian { get; set; }


        public override string ToString()
        {
            string kq = "\nMặt trời: \n\tHiện tại: " + MtHt + "\tCông suất lớn nhất: " + MtCsln + "\tThiết kế: " + MtTk + "\tSản lượng ngày: " + MtSln +
                    "\nGió: \n\tHiện tại: " + GHt + "\tCông suất lớn nhất: " + GCsln + "\tThiết kế: " + GTk + "\tSản Lượng ngày: " + GSln +
                    "\nSinh khối: \n\tHiện tại: " + SkHt + "\tCông suất lớn nhất: " + SkCsln + "\tThiết kế: " + SkTk + "\tSản lượng ngày: " + SkSln +
                    "\nTổng \n\tHiện tại: " + THt + "\tCông suất lớn nhất: " + TCsln + "\tThiết kế: " + TTk + "\tSản lượng ngày: " + TSln;
            return kq;
        }

    }
}
