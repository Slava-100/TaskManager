﻿using TaskManager.Enums;

namespace TaskManager
{
    public class Issue
    {
        public List<int> BlockedByCurrentIssue { get; set; }

        public List<int> BlockingIssues { get; set; }

        public int NumberIssue { get; set; }

        public string Description { get; set; }

        public long IdUser { get; set; }

        public string Comment { get; set; }

        public IssueStatus Status { get; set; }

        public bool IsAssignable =>  (Status == IssueStatus.Backlog || Status == IssueStatus.UserStory) && IdUser == 0;

        public Issue(int numberIssue, string description)
        {
            BlockedByCurrentIssue = new List<int>();
            BlockingIssues = new List<int>();
            NumberIssue = numberIssue;
            Description = description;
        }

        public Issue()
        {
            BlockedByCurrentIssue = new List<int>();
            BlockingIssues = new List<int>();
        }

        public Issue(List<int> blockedByCurrentIssue, List<int> blockingIssues, int numberIssue, string description, long idUser, string comment, IssueStatus status)
        {
            BlockedByCurrentIssue = blockedByCurrentIssue;
            BlockingIssues = blockingIssues;
            NumberIssue = numberIssue;
            Description = description;
            IdUser = idUser;
            Comment = comment;
            Status = status;
        }

        public override bool Equals(object? obj)
        {
            return obj is Issue issue &&
                   BlockedByCurrentIssue.SequenceEqual(issue.BlockedByCurrentIssue) &&
                   BlockingIssues.SequenceEqual(issue.BlockingIssues) &&
                   NumberIssue == issue.NumberIssue &&
                   Description == issue.Description &&
                   IdUser == issue.IdUser &&
                   Comment == issue.Comment &&
                   Status == issue.Status;
        }

        public override string ToString()
        {
            return $"Номер Задачи:\t {NumberIssue} " +
                   $"\nОписание задачи:\t {Description} " +
                   $"\nСтатус задачи:\t {Status}";
        }
    }
}
