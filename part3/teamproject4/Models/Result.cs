﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproject4.Models
{
    class Result
    {
        public int plastic { get; set; }
        public int paper { get; set; }
        public int can { get; set; }
        public DateTime date { get; set; }

        public static readonly string RESULT_INSERT_QUERY = @"INSERT INTO [dbo].[result]
                                                                           ([plastic]
                                                                           ,[paper]
                                                                           ,[can]
                                                                           ,[date])
                                                                     VALUES
                                                                           (plastic, int,
                                                                           ,paper, int,
                                                                           ,can, int,
                                                                           ,[date], datetime)";

        public static readonly string RESULT_SELECT_PLASTIC = @"SELECT 
                                                                SUM(CASE WHEN [plastic] = 1 THEN 1 ELSE 0 END) AS plastic_count
                                                                FROM [smart_factory].[dbo].[result]
                                                                WHERE CONVERT(date, [date]) = CONVERT(date, GETDATE());";
        public static readonly string RESULT_SELECT_PAPER = @"SELECT 
                                                                SUM(CASE WHEN [paper] = 1 THEN 1 ELSE 0 END) AS plastic_count
                                                                FROM [smart_factory].[dbo].[result]
                                                                WHERE CONVERT(date, [date]) = CONVERT(date, GETDATE());";
        public static readonly string RESULT_SELECT_CAN = @"SELECT 
                                                                SUM(CASE WHEN [can] = 1 THEN 1 ELSE 0 END) AS plastic_count
                                                                FROM [smart_factory].[dbo].[result]
                                                                WHERE CONVERT(date, [date]) = CONVERT(date, GETDATE());";

        public static readonly string DELETE_QUERY = @"DELETE FROM [dbo].[result]";

    }
}
