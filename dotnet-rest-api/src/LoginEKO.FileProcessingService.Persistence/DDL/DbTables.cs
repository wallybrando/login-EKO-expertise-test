namespace LoginEKO.FileProcessingService.Persistence.DDL
{
    public static class DbTables
    {
        public static class FileMetadata
        {
            public const string CreateTableSql =
                """
                CREATE TABLE IF NOT EXISTS blob_metadata (
                id UUID       primary key,
                filename      TEXT not null,
                extension     TEXT not null,
                size_in_bytes BIGINT not null,
                binary_object BYTEA not null,
                md5_hash      TEXT not null,
                created_date  TIMESTAMP not null,
                UNIQUE (id, filename, md5_hash));
                """;
        }

        public static class TractorTelemetry
        {
            public const string CreateTableSql =
                """
                CREATE TABLE IF NOT EXISTS tractor_telemetry (
                id                                  UUID DEFAULT uuid_generate_v4()  primary key,
                serial_number                       TEXT NOT NULL,
                date                                TIMESTAMP NOT NULL,
                gps_longitude                       DOUBLE PRECISION NOT NULL,
                gps_latitude                        DOUBLE PRECISION NOT NULL,
                total_working_hours                 DOUBLE PRECISION NOT NULL,
                engine_speed_in_rpm                 INT NOT NULL,
                engine_load_in_percentage           DOUBLE PRECISION NOT NULL,
                fuel_consumption_per_hour           DOUBLE PRECISION NULL,
                ground_speed_gearbox_in_kmh         DOUBLE PRECISION NOT NULL,
                ground_speed_radar_in_kmh           INT NULL,
                coolant_temperature_in_celsius      INT NOT NULL,
                speed_front_pto_in_rpm              INT NOT NULL,
                speed_rear_pto_in_rpm               INT NOT NULL,
                current_gear_shift                  SMALLINT NULL,
                ambient_temperature_in_celsius      DOUBLE PRECISION NOT NULL,
                parking_break_status                parkingbreakstatus NOT NULL,
                transverse_differential_lock_status transversedifferentiallockstatus NOT NULL,
                all_wheel_drive_status              wheeldrivestatus NOT NULL,
                actual_status_of_creeper            boolean NOT NULL);
                """;
        }

        public static class CombineTelemetry
        {
            public const string CreateTableSql = "";
        }
    }
}
