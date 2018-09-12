using Blog.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public interface ICommentRepository
    {
        Comment GetCommentById(string id);
        int SaveComment(Comment comment);
        int DeleteComment(Comment comment);
        int UpdateComment(Comment comment);
    }
}
