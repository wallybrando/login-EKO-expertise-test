using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.DDL
{
    public static class DbTypes
    {
        public const string CreateParkingBreakStatusType = """
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'parkingbreakstatus') THEN
                    CREATE TYPE parkingbreakstatus AS ENUM ('3');
                END IF;
            END $$;
            """;

        public const string CreateTransverseDifferentialLockStatusType = """
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'transversedifferentiallockstatus') THEN
                    CREATE TYPE transversedifferentiallockstatus AS ENUM ('0');
                END IF;
            END $$;
            """;

        public const string CreateWheelDriveStatusType = """
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'wheeldrivestatus') THEN
                    CREATE TYPE wheeldrivestatus AS ENUM ('Inactive', 'Active', '2');
                END IF;
            END $$;



            """;
    }
}
