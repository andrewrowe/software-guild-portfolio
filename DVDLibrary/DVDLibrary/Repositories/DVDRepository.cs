using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DVDLibrary.Config;
using DVDLibrary.Models;
using DVDLibrary.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DVDLibrary.Repositories
{
    public class DVDRepository
    {
        public List<DVD> GetAllDVDs()
        {
            List<DVD> DVDs = new List<DVD>();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "GetAllDVDs";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVDs.Add(PopulateDVDFromDataReader(dr));
                    }
                }
            }

            return DVDs;
        }

        public DVDInfoVM GetDVDInfo(string title)
        {
            DVDInfoVM dvdInfoVM = new DVDInfoVM();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DVDSelectByTitle";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Connection = cn;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dvdInfoVM.DVD = PopulateDVDFromDataReader(dr);
                        dvdInfoVM.Personnel.Add(PopulatePersonnelFromDataReader(dr));
                    }
                }

                return dvdInfoVM;
            }
        }

        public void AddDVD(DVDInfoVM dvdInfoVM)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "CheckIfStudioExists";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudioName", dvdInfoVM.DVD.Studio);
                cmd.Connection = cn;
                cn.Open();
                int studioCount = (int)cmd.ExecuteScalar();
                cn.Close();

                cmd.Parameters.AddWithValue("@DVDTitle", dvdInfoVM.DVD.Title);
                cmd.Parameters.AddWithValue("@ReleaseDate", dvdInfoVM.DVD.ReleaseDate);
                cmd.Parameters.AddWithValue("@RatingID", (int)dvdInfoVM.DVD.MPAARating);
                cmd.Parameters.AddWithValue("@MyRating", dvdInfoVM.DVD.UserRating);
                cmd.Parameters.AddWithValue("@URL", dvdInfoVM.DVD.URL);

                if (studioCount > 0)
                {
                    cmd.CommandText = "DVDInsertWithoutStudio";
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandText = "DVDInsertWithStudio";
                    cmd.CommandType = CommandType.StoredProcedure;
                }

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            SavePersonnel(dvdInfoVM);
        }

        public void RemoveDVD(DVDInfoVM dvdInfoVM)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DVDDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DVDTitle", dvdInfoVM.DVD.Title);
                cmd.Connection = cn;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                foreach (var person in dvdInfoVM.Personnel)
                {
                    DeletePersonnel(person);
                }
            }
        }

        public void DeletePersonnel(Personnel person)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "PersonnelDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonnelName", person.Name);
                cmd.Connection = cn;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void SavePersonnel(DVDInfoVM dvdInfoVM)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                foreach (var person in dvdInfoVM.Personnel)
                {
                    var cmd = new SqlCommand();
                    cmd.CommandText = "CheckIfRoleExists";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Role", person.Role);
                    cmd.Connection = cn;

                    cn.Open();
                    int roleCount = (int)cmd.ExecuteScalar();
                    cn.Close();

                    cmd.Parameters.AddWithValue("@DVDTitle", dvdInfoVM.DVD.Title);
                    cmd.Parameters.AddWithValue("@PersonnelName", person.Name);

                    if (roleCount > 0)
                    {
                        cmd.CommandText = "PersonnelInsert";
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        cmd.CommandText = "PersonnelAndRoleInsert";
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        private DVD PopulateDVDFromDataReader(SqlDataReader dr)
        {
            DVD dvd = new DVD();

            dvd.ID = (int)dr["DVD_ID"];
            dvd.MPAARating = (MPAARating)Enum.ToObject(typeof(MPAARating), dr["RatingID"]);
            dvd.ReleaseDate = (DateTime)dr["ReleaseDate"];
            dvd.Title = dr["Title"].ToString();
            dvd.Studio = dr["StudioName"].ToString();
            dvd.URL = dr["PictureURL"].ToString();
            dvd.UserRating = (int) dr["MyRating"];

            return dvd;
        }

        private Personnel PopulatePersonnelFromDataReader(SqlDataReader dr)
        {
            Personnel person = new Personnel();

            person.Name = dr["Name"].ToString();
            person.ID = (int) dr["PersonnelID"];
            person.Role = dr["Role"].ToString();
            person.RoleID = (int) dr["RoleID"];

            return person;
        }
    }
}