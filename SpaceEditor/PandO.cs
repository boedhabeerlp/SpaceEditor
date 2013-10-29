﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace SpaceEditor
{
    class PandO
    {
        public coord position = new coord();
        public coord forward = new coord();
        public coord up = new coord();

        public void loadFromXML(XmlNode node)
        {
            this.position.loadFromXML(node.SelectSingleNode("Position"));
            this.forward.loadFromXML(node.SelectSingleNode("Forward"));
            this.up.loadFromXML(node.SelectSingleNode("Up"));
        }

        public TreeNode getTreeNode(){
            TreeNode node =  new TreeNode("Position / Orientation");
            node.Nodes.Add(this.position.getTreeNode("Position"));
            node.Nodes.Add(this.position.getTreeNode("Forward"));
            node.Nodes.Add(this.position.getTreeNode("Up"));
            node.Tag = this;
            return node;
        }

        public string getXML()
        {
            string xml = "<PositionAndOrientation>\r\n";
            xml += this.position.getXML("Position");
            xml += this.forward.getXML("Forward");
            xml += this.up.getXML("Up");
            xml += "</PositionAndOrientation>\r\n";
            return xml;
        }
    }
}
