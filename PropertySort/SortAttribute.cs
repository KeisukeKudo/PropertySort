﻿using System;

namespace PropertySort {

    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false, Inherited = false)]
    public class SortAttribute : Attribute {

        private readonly int Index;

        public SortAttribute(int index) {
            this.Index = index;
        }

        public int SortIndex {
            get { return this.Index; }
        }

    }
}
