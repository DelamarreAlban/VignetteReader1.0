using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignetteReader1._0
{
    public class Partition
    {
        string id;
        string name;
        List<Node> nodes = new List<Node>();

        public Partition(string _name, string _id)
        {
            name = _name;
            id = _id;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Node> Nodes
        {
            get { return nodes; }
            set { value = nodes; }
        }

        public string getType()
        {
            return "uml:ActivityPartition";
        }

        public string getNodes()
        {
            string s = "";
            foreach (Node n in nodes)
                s += n.Id + " ";
            return s.Substring(0, s.Length - 1);
        }

        public string getXml()
        {
            string xmlElement = "<group";
            xmlElement += " xmi:type=" + "\"" + getType() + "\"";
            xmlElement += " xmi:id=" + "\"" + Id + "\"";
            xmlElement += " name=" + "\"" + name + "\"";
            if (Nodes.Count != 0)
                xmlElement += " node=" + "\"" + getNodes() + "\"";
            xmlElement += "/>";
            return xmlElement;
        }
        //<group xmi:type="uml:ActivityPartition" xmi:id="_dNPt9oa_EeaGQIHr6EmBRw" name="Teacher" 
        //    node="_dNPthIa_EeaGQIHr6EmBRw _dNPtiIa_EeaGQIHr6EmBRw" represents="_dNQUFYa_EeaGQIHr6EmBRw"/>
    }
}
