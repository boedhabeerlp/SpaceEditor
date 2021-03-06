﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SpaceEditor
{
    class EntityBase
    {
        public string EntityId;
        public string XMLType = null;
        public string PersistentFlags;
        public PandO PositionAndOrientation = new PandO();
        public string displayType = "Entity";
        public string actualType = "Entity";
        public Sector parent_sector = null;

        public EntityBase(Sector parent)
        {
            this.parent_sector = parent;
        }

        public void loadFromXML(XmlNode node)
        {
            this.PositionAndOrientation.loadFromXML(node.SelectSingleNode("PositionAndOrientation"));
            this.PersistentFlags = node.SelectSingleNode("PersistentFlags").InnerText;
            this.EntityId = node.SelectSingleNode("EntityId").InnerText;
            XmlNode attr = node.Attributes["xsi:type"];
            if (attr != null && !string.IsNullOrEmpty(attr.Value))
                this.XMLType = attr.Value;
            parent_sector.main_form.update_status("Loaded: " + this.EntityId);
        }

        public TreeNode getTreeNode()
        {
            TreeNode node = new TreeNode(this.displayType);
            node.Nodes.Add("[EntityID] " + this.EntityId);
            node.Nodes.Add(this.PositionAndOrientation.getTreeNode());
            node.Nodes.Add("[PersistentFlags] " + this.PersistentFlags);            
            return node;
        }

        public string getXML()
        {
            string xml = "";
            xml += "<EntityId>"+this.EntityId+"</EntityId>\r\n";
            xml += "<PersistentFlags>"+this.PersistentFlags+"</PersistentFlags>\r\n";
            xml += this.PositionAndOrientation.getXML();
            this.parent_sector.main_form.update_status("Got Xml For:" + this.displayType);         
            return xml;
        }

        public static string replace_id(Match m)
        {            
            return "<EntityId>" + EntityBase.generate_new_id() + "</EntityId>";
           
        }

        public void new_id()
        {
            this.EntityId = EntityBase.generate_new_id();
        }

        public static string generate_new_id()
        {
            //Console.WriteLine("Generating new id");
            Random rnd = Sector.rnd;
            long min = long.MinValue;
            long max = long.MaxValue;
            ulong uRange = (ulong)(max - min);
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                rnd.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);
            long result = (long)(ulongRand % uRange) + min;
            //long id = long.Parse(this.EntityId);
            //id += 1;
            return result.ToString();
            
        }

    }
}
