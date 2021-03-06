﻿namespace GoatJira.Model.Jira.JiraIssue
{
    using System;
    using Atlassian.Jira;

    class AtlassianJiraIssueModelService : IJiraIssueModelService
    {
        private readonly Issue issue;

        public AtlassianJiraIssueModelService(Issue Issue)
        {
            issue = Issue;
        }

        public JiraIssueModel Read()
        {
            JiraIssueModel result = new JiraIssueModel
            {
                Key = issue.Key.Value,
                Type = issue.Type.Name,
                Priority = issue.Priority.Name,
                Components = GetComponents(issue),
                Labels = GetLabels(issue),
                Project = issue.Project,
                Assignee = issue.Assignee,
                Reporter = issue.Reporter,
                CreatedAt = issue.Created,
                UpdatedAt = issue.Updated,
                Description = issue.Description,
                Summary = issue.Summary,
                Status = issue.Status.Name,
                Resolution = issue.Resolution?.Name,
                ResolutionDate = issue.ResolutionDate,
                DueDate = issue.DueDate,
            };

            foreach (var cf in issue.CustomFields)
            {
                if (cf.Values.Length == 0)
                    result.CustomFields.Add(cf.Name, "<NULL>");
                else
                    result.CustomFields.Add(cf.Name, cf.Values[0]);
            }

            //not sure whether this should be here or in JiraIssueModel as derived attribute
            try { result.DefinitionOfDone = result.CustomFields["Definition of Done"]; } catch { }
            try { result.AcceptanceCriteria = result.CustomFields["Acceptance Criteria"]; } catch { } 

            return result;
        }

        public void Save(JiraIssueModel LoginInformation)
        {
            throw new NotImplementedException();
        }


        private string GetComponents(Issue issue)
        {
            string result = "";
            foreach (var Component in issue.Components)
                result += Component.Name + ", ";
            if (!String.IsNullOrWhiteSpace(result))
                result = result.Substring(0, result.Length - 2);
            return result;
        }

        private string GetLabels(Issue issue)
        {
            string result = "";
            foreach (var Label in issue.Labels)
                result += result == "" ? Label : ", "+Label;
            return result;
        }





    }
}
