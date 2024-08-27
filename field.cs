using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1_conccurent_programming
{
    class field
    {

        private string name;
        private int credits;
        private double gpa;

        public field(string name, int credits, double gpa)
        {
            this.name = name;
            this.credits = credits;
            this.gpa = gpa;
        }
        public field() { }
        public string GetName()
        {
            return name;
        }

        public int GetNumber()
        {
            return credits;
        }

        public double GetGpa()
        {
            return gpa;
        }

        public override string ToString()
        {
            string line = string.Format("{0,-20} {1,-10} {2,-10}", name, credits, gpa);
            return line;
        }


        public int CompareTo(field other)
        {
            if (other == null)
                return 1; // Null is considered greater

            // Compare based on the "name" property in descending order
            return string.Compare(other.name, name, StringComparison.OrdinalIgnoreCase);
        }



    }
}
