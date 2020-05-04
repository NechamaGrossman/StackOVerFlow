using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackOverFlow.Data
{
    public class QuestionRepository

    {
        string _connectionString;
        public QuestionRepository(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        public List<Question> GetQuestions()
        {
            using (var context = new QuestionContext(_connectionString))
            {
                return context.Questions.OrderByDescending(q => q.DatePosted).ToList();
            }
        }
        public Question GetQuestionById(int QuestionId)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                Question question = context.Questions.FirstOrDefault(q => q.Id == QuestionId);
                question.Answers = GetAnswerForId(QuestionId);
                return question;

            }
        }
        public List<Tag> GetTagsForQuestionId(int QuestionId)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                List<QuestionTags> questionTags= context.QuestionsTags.Where(qt => qt.QuestionId == QuestionId).ToList();
                if(questionTags==null)
                {
                    return null;
                }
                List<Tag> tags = new List<Tag>();
                foreach(QuestionTags qt in questionTags)
                {
                    tags.Add(context.Tags.FirstOrDefault(t => t.Id == qt.TagId));
                }

                return tags;
            }
        }
        public int GetLikesCountForId(int Id)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                return context.Likes.Where(l => l.Like == true && l.QuestionId == Id).ToList().Count;

            }
        }
        public void AddLikes(Likes likes)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                context.Likes.Add(likes);
                context.SaveChanges();

            }
        }
        public List<Answers> GetAnswerForId(int Id)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                return context.Answers.Where(a => a.QuestionId == Id).ToList();
            }
        }
        public bool IsQuestionLikedByUser(int questionId, int userId)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                Likes l  = context.Likes.FirstOrDefault(likes => likes.QuestionId == questionId && likes.UserId == userId);
                if(l is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public void AddUser(User u, string PassWord)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(PassWord);
            u.Hash = hash;
            using (var context = new QuestionContext(_connectionString))
            {
                context.User.Add(u);
                context.SaveChanges();
            }
        }
        public User Login(string Email, string Password)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                User u = GetUserByEmail(Email);
                if (u == null)
                {
                    return null;
                }
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(Password, u.Hash);
                if (isValidPassword)
                {
                    return u;
                }
                return null;
            }
        }
        public User GetUserByEmail(string Email)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                return context.User.FirstOrDefault(u => u.Email == Email);
            }
        }
        public void AddAnswer(Answers answers)
        {
            using (var context = new QuestionContext(_connectionString))
            {
                context.Answers.Add(answers);
                context.SaveChanges();
            }
        }
        public void AskQuestion(Question question, List<string> Tags)
        {
            using (var ctx = new QuestionContext(_connectionString))
            {
                ctx.Questions.Add(question);
                foreach (string tag in Tags)
                {
                    Tag t = GetTag(tag);
                    int tagId;
                    if (t == null)
                    {
                        tagId = AddTag(tag);
                    }
                    else
                    {
                        tagId = t.Id;
                    }
                    ctx.QuestionsTags.Add(new QuestionTags
                    {
                        QuestionId = question.Id,
                        TagId = tagId
                    });
                }

                ctx.SaveChanges();
            }
        }
        private int AddTag(string name)
        {
            using (var ctx = new QuestionContext(_connectionString))
            {
                var tag = new Tag { Name = name };
                ctx.Tags.Add(tag);
                ctx.SaveChanges();
                return tag.Id;
            }


        }
        private Tag GetTag(string name)
        {
            using (var ctx = new QuestionContext(_connectionString))
            {
                return ctx.Tags.FirstOrDefault(t => t.Name == name);
            }
        }
    }
}
