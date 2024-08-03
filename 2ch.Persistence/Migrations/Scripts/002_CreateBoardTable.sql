CREATE TABLE IF NOT EXISTS Board (
    "BoardId" uuid PRIMARY KEY,
    "UserId" uuid NOT NULL,
    "Name" TEXT NOT NULL,
    "Description" TEXT NULL,
    FOREIGN KEY ("UserId") REFERENCES AnonymousUsers("UserId")
);
