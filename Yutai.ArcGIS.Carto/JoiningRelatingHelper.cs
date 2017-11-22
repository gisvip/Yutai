using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto
{
    internal class JoiningRelatingHelper
    {
        public static void JoinTableLayer(ILayer pLayer, string pJoinFieldName, ITable pTable, string pToFieldName)
        {
            try
            {
                IAttributeTable table = pLayer as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    if (pTable is IStandaloneTable)
                    {
                        pTable = (pTable as IStandaloneTable).Table;
                    }
                    IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                    IRelationshipClass relClass = factory.Open("TabletoLayer", (IObjectClass) pTable, pToFieldName,
                        (IObjectClass) attributeTable, pJoinFieldName, "forward", "backward",
                        esriRelCardinality.esriRelCardinalityOneToMany);
                    ((IDisplayRelationshipClass) pLayer).DisplayRelationshipClass(relClass,
                        esriJoinType.esriLeftOuterJoin);
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void JoinTableLayer(ITable pSrcTable, string pSrcFieldName, ITable pToTable, string pToFieldName)
        {
            try
            {
                ITable attributeTable = null;
                ITable displayTable = null;
                IDisplayTable table3 = pSrcTable as IDisplayTable;
                if (table3 != null)
                {
                    displayTable = table3.DisplayTable;
                }
                if (displayTable is IRelQueryTable)
                {
                    attributeTable = displayTable;
                }
                else
                {
                    IAttributeTable table4 = pSrcTable as IAttributeTable;
                    if (table4 == null)
                    {
                        return;
                    }
                    attributeTable = table4.AttributeTable;
                }
                if (pToTable is IStandaloneTable)
                {
                    pToTable = (pToTable as IStandaloneTable).Table;
                }
                IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                IRelationshipClass relClass = factory.Open("TabletoLayer", (IObjectClass) pToTable, pToFieldName,
                    (IObjectClass) attributeTable, pSrcFieldName, "forward", "backward",
                    esriRelCardinality.esriRelCardinalityOneToMany);
                ((IDisplayRelationshipClass) pSrcTable).DisplayRelationshipClass(relClass, esriJoinType.esriLeftOuterJoin);
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void RelateTableLayer(ILayer pSrcLayer, string pSrcFieldName, ITable pToTable, string pToFieldName)
        {
            try
            {
                IAttributeTable table = pSrcLayer as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    if (pToTable is IStandaloneTable)
                    {
                        pToTable = (pToTable as IStandaloneTable).Table;
                    }
                    IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                    IRelationshipClass relationshipClass = factory.Open("TabletoLayer", (IObjectClass) pToTable,
                        pToFieldName, (IObjectClass) attributeTable, pSrcFieldName, "forward", "backward",
                        esriRelCardinality.esriRelCardinalityOneToMany);
                    ((IRelationshipClassCollectionEdit) pSrcLayer).AddRelationshipClass(relationshipClass);
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void RelateTableLayer(string pRelName, ITable pSrcTable, string pSrcFieldName, ITable pToTable,
            string pToFieldName)
        {
            try
            {
                IAttributeTable table = pSrcTable as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                    IRelationshipClass relationshipClass = factory.Open(pRelName, (IObjectClass) pToTable, pToFieldName,
                        (IObjectClass) attributeTable, pSrcFieldName, "forward", "backward",
                        esriRelCardinality.esriRelCardinalityOneToMany);
                    ((IRelationshipClassCollectionEdit) pSrcTable).AddRelationshipClass(relationshipClass);
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static bool TableIsJoinLayer(ILayer pLayer, ITable pTable)
        {
            try
            {
                IDisplayTable table = pLayer as IDisplayTable;
                if (table != null)
                {
                    ITable displayTable = table.DisplayTable;
                    string str = (pTable as IDataset).Name.ToLower();
                    while (displayTable is IRelQueryTable)
                    {
                        IRelQueryTable table3 = displayTable as IRelQueryTable;
                        if ((table3.DestinationTable as IDataset).Name.ToLower() == str)
                        {
                            return true;
                        }
                        displayTable = table3.SourceTable;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        public static bool TableIsJoinLayer(ITable pTable, ITable pRelTable)
        {
            try
            {
                IDisplayTable table = pTable as IDisplayTable;
                if (table != null)
                {
                    ITable displayTable = table.DisplayTable;
                    string str = (pRelTable as IDataset).Name.ToLower();
                    while (displayTable is IRelQueryTable)
                    {
                        IRelQueryTable table3 = displayTable as IRelQueryTable;
                        if ((table3.DestinationTable as IDataset).Name.ToLower() == str)
                        {
                            return true;
                        }
                        displayTable = table3.SourceTable;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        public static bool TableIsRelateLayer(ILayer pSrcLayer, ITable pRelTable)
        {
            try
            {
                IRelationshipClassCollection classs = pSrcLayer as IRelationshipClassCollection;
                if (classs != null)
                {
                    IEnumRelationshipClass class2 = classs.FindRelationshipClasses(pRelTable as IObjectClass,
                        esriRelRole.esriRelRoleAny);
                    class2.Reset();
                    if (class2.Next() != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        public static bool TableIsRelateLayer(ITable pSrcTable, ITable pRelTable)
        {
            try
            {
                IRelationshipClassCollection classs = pSrcTable as IRelationshipClassCollection;
                if (classs != null)
                {
                    IEnumRelationshipClass class2 = classs.FindRelationshipClasses(pRelTable as IObjectClass,
                        esriRelRole.esriRelRoleAny);
                    class2.Reset();
                    if (class2.Next() != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }
    }
}