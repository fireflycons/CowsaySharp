﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsayLibrary
{
    class CowFaces
    {
        public enum cowFaces
        {
            defaultFace,
            borg,
            dead,
            greedy,
            paranoid,
            stoned,
            tired,
            wired,
            young
        }

        static public Dictionary<cowFaces, CowFace> faces { get; private set; }
        private CowFace defaultFace { get; set; }
        private CowFace borg { get; set; }
        private CowFace dead { get; set; }
        private CowFace greedy { get; set; }
        private CowFace paranoid { get; set; }
        private CowFace stoned { get; set; }
        private CowFace tired { get; set; }
        private CowFace wired { get; set; }
        private CowFace young { get; set; }

        static public CowFace getCowFace(cowFaces face)
        {
            populateFacesDictionary();
            CowFace value = new CowFace();
            faces.TryGetValue(face, out value);

            return value;
        }

        static private void populateFacesDictionary()
        {
            faces = new Dictionary<cowFaces, CowFace>();

            CowFace defaultFace = new CowFace("oo");
            CowFace borg = new CowFace("==");
            CowFace dead = new CowFace("xx", "U ");
            CowFace greedy = new CowFace("$$");
            CowFace paranoid = new CowFace("@@");
            CowFace stoned = new CowFace("**", "U ");
            CowFace tired = new CowFace("--");
            CowFace wired = new CowFace("OO");
            CowFace young = new CowFace("..");

            faces.Add(cowFaces.defaultFace, defaultFace);
            faces.Add(cowFaces.borg, borg);
            faces.Add(cowFaces.dead, dead);
            faces.Add(cowFaces.greedy, greedy);
            faces.Add(cowFaces.paranoid, paranoid);
            faces.Add(cowFaces.stoned, stoned);
            faces.Add(cowFaces.tired, tired);
            faces.Add(cowFaces.wired, wired);
            faces.Add(cowFaces.young, young);
        }

    }
}
