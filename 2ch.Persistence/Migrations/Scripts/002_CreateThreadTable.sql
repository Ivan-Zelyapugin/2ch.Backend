CREATE TABLE IF NOT EXISTS thread (
    "ThreadId" uuid PRIMARY KEY,
    "BoardId" uuid NOT NULL,
    "Title" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY ("BoardId") REFERENCES board("BoardId")
);
