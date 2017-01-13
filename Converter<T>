using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;


namespace EntityHelpers
{
    public class Converter<T>
        where T : class
    {
        private class MemberDetails
        {
            public MemberDetails(string dbName, PropertyInfo pi, bool isTableColumn)
            {
                Name = dbName;
                PropertyInfo = pi;
                IsTableColumn = isTableColumn;
            }

            public string Name { get; }

            public PropertyInfo PropertyInfo { get; }

            public bool IsTableColumn { get; }
        }

        private readonly Type _entityType;
        private readonly IList<MemberDetails> _fieldDetails;
        private readonly IList<MemberDetails> _columns;

        public Converter()
        {
            this._entityType = typeof(T);
            this._fieldDetails = new List<MemberDetails>();
            this._columns = new List<MemberDetails>();

            this.AssignTableName();
            this.AssignColumnsAndProperties();
        }

        public string TableName { get; private set; }

        public IEnumerable<string> ColumnNames { get; private set; }

        public IEnumerable<string> Properties { get; private set; }

        public DataTable MapToDataTable(IEnumerable<T> entities)
        {
            var dt = GetDataTableRepresentation();

            foreach (var e in entities)
            {
                var row = dt.NewRow();
                dt.Rows.Add(row);
            }

            return dt;
        }

        public void MapToDataRow(T entity, ref DataRow row)
        {
            if (row == null)
            {
                return;
            }

            foreach (var c in _columns)
            {
                var value = c.PropertyInfo.GetValue(entity, null) ?? DBNull.Value;

                row[c.Name] = value;
            }
        }

        public IEnumerable<T> MapToEntitySet(DataTable table)
        {
            return MapToEntitySet(table.AsEnumerable());
        }

        public IEnumerable<T> MapToEntitySet(IEnumerable<DataRow> rows)
        {
            var entitySet = new List<T>();


            foreach (DataRow row in rows)
            {
                var entity = MapToEntity(row);

                entitySet.Add(entity);
            }

            return entitySet;
        }

        public T MapToEntity(DataRow row)
        {
            var entity = Activator.CreateInstance<T>();

            foreach (var c in _columns)
            {
                var value = row[c.Name];

                c.PropertyInfo.SetValue(entity, value == DBNull.Value ? null : value);
            }

            return entity;
        }

        public DataTable GetDataTableRepresentation()
        {
            var dt = new DataTable(this.TableName);

            foreach (var p in _columns)
            {
                string cName = p.Name;
                Type cType = null;

                var nt = Nullable.GetUnderlyingType(p.PropertyInfo.PropertyType);
                cType = nt ?? p.PropertyInfo.PropertyType;

                dt.Columns.Add(cName, cType);
            }

            return dt;
        }

        private void AssignTableName()
        {
            var ca = this._entityType.GetCustomAttributes(typeof(TableAttribute), true);

            if (ca.Any())
            {
                TableName = ((TableAttribute)ca.First()).Name;
            }
            else
            {
                TableName = _entityType.Name;
            }
        }

        private void AssignColumnsAndProperties()
        {


            foreach (var prop in _entityType.GetProperties())
            {
                var a = prop.GetCustomAttributes(typeof(ColumnAttribute), true);

                var attr = (ColumnAttribute)a.FirstOrDefault();

                var isTableColumn = attr != null;

                var fd = new MemberDetails(string.IsNullOrEmpty(attr?.Name) ? prop.Name : attr.Name, prop, isTableColumn);

                _fieldDetails.Add(fd);

                if (isTableColumn)
                    _columns.Add(fd);
            }

            this.ColumnNames = this._columns.Select(x => x.Name);
            this.Properties = this._fieldDetails.Select(x => x.Name);

        }
    }
}
