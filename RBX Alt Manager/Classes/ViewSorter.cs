using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    class ViewSorter : IComparer
    {
        SortOrder _Order;

        public ViewSorter(SortOrder order)
        {
            _Order = order;
        }

        public int Compare(object x, object y)
        {
            // perform you desired comparison depending on the _Order
            Console.WriteLine($"{x}, {y}");

            return 0;
        }
    }
}
