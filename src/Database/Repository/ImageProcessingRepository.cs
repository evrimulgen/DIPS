﻿using DIPS.Database.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Database.Repository
{
    public class ImageProcessingRepository
    {
        public void insertTechnique(String name, XDocument doc)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_InsertTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@technique", SqlDbType.Xml).Value = doc.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Technique> getAllTechnique()
        {
            List<Technique> list = null;

            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveAllTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    if (list == null) list = new List<Technique>();
                    Technique technique = new Technique();
                    technique.ID = data.GetInt32(data.GetOrdinal("ID"));
                    technique.Name = data.GetString(data.GetOrdinal("name"));
                    list.Add(technique);
                }
                data.Close();
            }

            return list;
        }

        public XDocument getSpecificTechnique(int id)
        {
            String data = null;
            XDocument xml = null;

            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveSelectedTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                data = cmd.ExecuteScalar().ToString();
            }

            if (data != null) xml = XDocument.Parse(data);
            return xml;
        }

        public void updateTechnique(int id, String name, XDocument technique)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_UpdateXmlTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@technique", SqlDbType.Xml).Value = technique.ToString();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
