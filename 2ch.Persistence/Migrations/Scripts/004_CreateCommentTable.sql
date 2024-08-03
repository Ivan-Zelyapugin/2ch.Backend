CREATE TABLE IF NOT EXISTS Comment (
    "CommentId" uuid PRIMARY KEY,
    "ThreadId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Content" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY ("ThreadId") REFERENCES Thread("ThreadId"),
    FOREIGN KEY ("UserId") REFERENCES AnonymousUsers("UserId")
);
