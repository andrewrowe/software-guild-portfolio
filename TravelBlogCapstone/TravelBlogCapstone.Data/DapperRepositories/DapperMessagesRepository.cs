using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.DapperRepositories
{
    public class DapperMessagesRepository : IMessagesRepository
    {
        public List<Message> GetAllForUser(string userId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT Messages.ID, Messages.SendDate, Messages.SendUserID, Messages.RecipientUserID, " +
                               "Messages.Subject, Messages.Body, Messages.IsRead, Messages.IsSenderDeleted, Messages.IsReceiverDeleted " +
                               "FROM Messages " +
                               "WHERE Messages.SendUserID = @SendUserID " +
                               "OR Messages.RecipientUserID = @RecipientUserID " +
                               "ORDER BY Messages.SendDate DESC";

                var parameters = new DynamicParameters();
                parameters.Add("SendUserID", userId);
                parameters.Add("RecipientUserID", userId);

                var result = cn.Query<Message>(query, parameters).ToList();

                return result;
            }
        }

        public Message Get(int messageId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT Messages.ID, Messages.SendDate, Messages.SendUserID, Messages.RecipientUserID, " +
                               "Messages.Subject, Messages.Body, Messages.IsRead, Messages.IsSenderDeleted, Messages.IsReceiverDeleted " +
                               "FROM Messages " +
                               "WHERE Messages.ID = @MessageID";

                var parameters = new DynamicParameters();
                parameters.Add("MessageID", messageId);

                var result = cn.Query<Message>(query, parameters).FirstOrDefault();

                return result;
            }
        }


        public Message Insert(Message message)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "INSERT INTO Messages(SendDate, SendUserID, RecipientUserID, Subject, Body, IsRead, IsSenderDeleted, IsReceiverDeleted) " +
                               "VALUES(@SendDate, @SendUserID, @RecipientUserID, @Subject, @Body, @IsRead, @IsSenderDeleted, @IsReceiverDeleted) " +
                               "SET @MessageID = SCOPE_IDENTITY(); ";

                var p = new DynamicParameters();
                p.Add("SendDate", message.SendDate);
                p.Add("SendUserID", message.SendUserId);
                p.Add("RecipientUserID", message.RecipientUserId);
                p.Add("Subject", message.Subject);
                p.Add("Body", message.Body);
                p.Add("IsRead", message.IsRead);
                p.Add("IsSenderDeleted", message.IsSenderDeleted);
                p.Add("IsReceiverDeleted", message.IsReceiverDeleted);
                p.Add("MessageID", DbType.Int32, direction: ParameterDirection.Output);

                cn.Execute(query, p);

                message.Id = p.Get<int>("MessageID");

            }
            return message;
        }

        public void Delete(int messageId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE Messages WHERE ID = @MessageID";
                var p = new DynamicParameters();
                p.Add("MessageID", messageId);
                cn.Execute(query, p);
            }
        }

        public void Update(Message message)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Messages SET SendDate = @SendDate, SendUserID = @SendUserID, " +
                               "RecipientUserID = @RecipientUserID, Subject = @Subject, Body = @Body, " +
                               "IsRead = @IsRead, IsSenderDeleted = @IsSenderDeleted, IsReceiverDeleted = @IsReceiverDeleted " +
                               "FROM Messages " +
                               "WHERE ID = @MessageID";
                var p = new DynamicParameters();
                p.Add("MessageID", message.Id);
                p.Add("SendDate", message.SendDate);
                p.Add("SendUserID", message.SendUserId);
                p.Add("RecipientUserID", message.RecipientUserId);
                p.Add("Subject", message.Subject);
                p.Add("Body", message.Body);
                p.Add("IsRead", message.IsRead);
                p.Add("IsSenderDeleted", message.IsSenderDeleted);
                p.Add("IsReceiverDeleted", message.IsReceiverDeleted);

                cn.Execute(query, p);
            }
        }
    }
}
