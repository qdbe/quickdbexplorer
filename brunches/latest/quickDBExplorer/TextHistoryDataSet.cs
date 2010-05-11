﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2407
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace quickDBExplorer {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class TextHistoryDataSet : DataSet {
        
        private TextHistoryDataSetsDataTable tableTextHistoryDataSets;
        
        public TextHistoryDataSet() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected TextHistoryDataSet(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["TextHistoryDataSets"] != null)) {
                    this.Tables.Add(new TextHistoryDataSetsDataTable(ds.Tables["TextHistoryDataSets"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TextHistoryDataSetsDataTable TextHistoryDataSets {
            get {
                return this.tableTextHistoryDataSets;
            }
        }
        
        public override DataSet Clone() {
            TextHistoryDataSet cln = ((TextHistoryDataSet)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["TextHistoryDataSets"] != null)) {
                this.Tables.Add(new TextHistoryDataSetsDataTable(ds.Tables["TextHistoryDataSets"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableTextHistoryDataSets = ((TextHistoryDataSetsDataTable)(this.Tables["TextHistoryDataSets"]));
            if ((this.tableTextHistoryDataSets != null)) {
                this.tableTextHistoryDataSets.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "TextHistoryDataSet";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/TextHistoryDataSet.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = true;
            this.EnforceConstraints = true;
            this.tableTextHistoryDataSets = new TextHistoryDataSetsDataTable();
            this.Tables.Add(this.tableTextHistoryDataSets);
        }
        
        private bool ShouldSerializeTextHistoryDataSets() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void TextHistoryDataSetsRowChangeEventHandler(object sender, TextHistoryDataSetsRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TextHistoryDataSetsDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnKeyNo;
            
            private DataColumn columnKeyValue;
            
            private DataColumn columnDataValue;
            
            internal TextHistoryDataSetsDataTable() : 
                    base("TextHistoryDataSets") {
                this.InitClass();
            }
            
            internal TextHistoryDataSetsDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn KeyNoColumn {
                get {
                    return this.columnKeyNo;
                }
            }
            
            internal DataColumn KeyValueColumn {
                get {
                    return this.columnKeyValue;
                }
            }
            
            internal DataColumn DataValueColumn {
                get {
                    return this.columnDataValue;
                }
            }
            
            public TextHistoryDataSetsRow this[int index] {
                get {
                    return ((TextHistoryDataSetsRow)(this.Rows[index]));
                }
            }
            
            public event TextHistoryDataSetsRowChangeEventHandler TextHistoryDataSetsRowChanged;
            
            public event TextHistoryDataSetsRowChangeEventHandler TextHistoryDataSetsRowChanging;
            
            public event TextHistoryDataSetsRowChangeEventHandler TextHistoryDataSetsRowDeleted;
            
            public event TextHistoryDataSetsRowChangeEventHandler TextHistoryDataSetsRowDeleting;
            
            public void AddTextHistoryDataSetsRow(TextHistoryDataSetsRow row) {
                this.Rows.Add(row);
            }
            
            public TextHistoryDataSetsRow AddTextHistoryDataSetsRow(int KeyNo, string KeyValue, string DataValue) {
                TextHistoryDataSetsRow rowTextHistoryDataSetsRow = ((TextHistoryDataSetsRow)(this.NewRow()));
                rowTextHistoryDataSetsRow.ItemArray = new object[] {
                        KeyNo,
                        KeyValue,
                        DataValue};
                this.Rows.Add(rowTextHistoryDataSetsRow);
                return rowTextHistoryDataSetsRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                TextHistoryDataSetsDataTable cln = ((TextHistoryDataSetsDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new TextHistoryDataSetsDataTable();
            }
            
            internal void InitVars() {
                this.columnKeyNo = this.Columns["KeyNo"];
                this.columnKeyValue = this.Columns["KeyValue"];
                this.columnDataValue = this.Columns["DataValue"];
            }
            
            private void InitClass() {
                this.columnKeyNo = new DataColumn("KeyNo", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnKeyNo);
                this.columnKeyValue = new DataColumn("KeyValue", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnKeyValue);
                this.columnDataValue = new DataColumn("DataValue", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDataValue);
                this.CaseSensitive = true;
            }
            
            public TextHistoryDataSetsRow NewTextHistoryDataSetsRow() {
                return ((TextHistoryDataSetsRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new TextHistoryDataSetsRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(TextHistoryDataSetsRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TextHistoryDataSetsRowChanged != null)) {
                    this.TextHistoryDataSetsRowChanged(this, new TextHistoryDataSetsRowChangeEvent(((TextHistoryDataSetsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TextHistoryDataSetsRowChanging != null)) {
                    this.TextHistoryDataSetsRowChanging(this, new TextHistoryDataSetsRowChangeEvent(((TextHistoryDataSetsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TextHistoryDataSetsRowDeleted != null)) {
                    this.TextHistoryDataSetsRowDeleted(this, new TextHistoryDataSetsRowChangeEvent(((TextHistoryDataSetsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TextHistoryDataSetsRowDeleting != null)) {
                    this.TextHistoryDataSetsRowDeleting(this, new TextHistoryDataSetsRowChangeEvent(((TextHistoryDataSetsRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveTextHistoryDataSetsRow(TextHistoryDataSetsRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TextHistoryDataSetsRow : DataRow {
            
            private TextHistoryDataSetsDataTable tableTextHistoryDataSets;
            
            internal TextHistoryDataSetsRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableTextHistoryDataSets = ((TextHistoryDataSetsDataTable)(this.Table));
            }
            
            public int KeyNo {
                get {
                    try {
                        return ((int)(this[this.tableTextHistoryDataSets.KeyNoColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("値は DBNull であるため、取得できません。", e);
                    }
                }
                set {
                    this[this.tableTextHistoryDataSets.KeyNoColumn] = value;
                }
            }
            
            public string KeyValue {
                get {
                    try {
                        return ((string)(this[this.tableTextHistoryDataSets.KeyValueColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("値は DBNull であるため、取得できません。", e);
                    }
                }
                set {
                    this[this.tableTextHistoryDataSets.KeyValueColumn] = value;
                }
            }
            
            public string DataValue {
                get {
                    try {
                        return ((string)(this[this.tableTextHistoryDataSets.DataValueColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("値は DBNull であるため、取得できません。", e);
                    }
                }
                set {
                    this[this.tableTextHistoryDataSets.DataValueColumn] = value;
                }
            }
            
            public bool IsKeyNoNull() {
                return this.IsNull(this.tableTextHistoryDataSets.KeyNoColumn);
            }
            
            public void SetKeyNoNull() {
                this[this.tableTextHistoryDataSets.KeyNoColumn] = System.Convert.DBNull;
            }
            
            public bool IsKeyValueNull() {
                return this.IsNull(this.tableTextHistoryDataSets.KeyValueColumn);
            }
            
            public void SetKeyValueNull() {
                this[this.tableTextHistoryDataSets.KeyValueColumn] = System.Convert.DBNull;
            }
            
            public bool IsDataValueNull() {
                return this.IsNull(this.tableTextHistoryDataSets.DataValueColumn);
            }
            
            public void SetDataValueNull() {
                this[this.tableTextHistoryDataSets.DataValueColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TextHistoryDataSetsRowChangeEvent : EventArgs {
            
            private TextHistoryDataSetsRow eventRow;
            
            private DataRowAction eventAction;
            
            public TextHistoryDataSetsRowChangeEvent(TextHistoryDataSetsRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public TextHistoryDataSetsRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}