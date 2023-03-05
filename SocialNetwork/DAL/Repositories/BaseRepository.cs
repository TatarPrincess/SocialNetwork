﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace SocialNetwork.DAL.Repositories;

public class BaseRepository
{
    protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            var result = connection.QueryFirstOrDefault<T>(sql, parameters);
            return result;
        }
    }

    protected List<T> Query<T>(string sql, object parameters = null)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            return connection.Query<T>(sql, parameters).ToList();
        }
    }

    protected int Execute(string sql, object parameters = null)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            return connection.Execute(sql, parameters);
        }
    }

    private IDbConnection CreateConnection()
    {
        //return new SQLiteConnection("Data Source = SocialNetwork/DAL/DB/social_network_db.db; Version = 3");
        return new SQLiteConnection("Data Source=C:\\Store\\C#\\SF\\SocialNetwork\\SocialNetwork\\DAL\\DB\\social_network_db.db;");
    }
}