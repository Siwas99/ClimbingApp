using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

namespace ClimbingApp.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;
        
        public CommentRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }

        public bool Insert(Comment Comment)
        {
            try
            {
                dbContext.Comments.Add(Comment);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Update(Comment Comment)
        {
            var existingComment = GetById(Comment.CommentId);

            if (existingComment == null)
                return false;

            try
            {
                existingComment.Commentary = Comment.Commentary;
                existingComment.User = Comment.User;
                existingComment.CommentObject = Comment.CommentObject;
                existingComment.CommentObjectId = Comment.CommentObjectId;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            var existingComment = GetById(id);

            if (existingComment == null)
                return false;

            try
            {
                dbContext.Comments.Remove(existingComment);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Comment> List()
        {
            return dbContext.Comments.ToList();
        }

        public Comment GetById(int id)
        {
            return (Comment)dbContext.Comments.Where(x => x.CommentId == id);
        }

    }
}
