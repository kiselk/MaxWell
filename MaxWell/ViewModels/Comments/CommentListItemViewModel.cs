using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Comments
{
    public class CommentListItemViewModel : BaseViewModel
    {

        public Comment Comment;

        public CommentListItemViewModel(Comment comment)
        {
            Comment = comment;


            if (comment != null)
            {
                PersonNameFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToPersonNameTask(comment.PersonId));
                PersonImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToPersonImageTask(comment.PersonId));
            }

        }

        public DateTime Date => Comment.Date;
        public TimeSpan Time => Comment.Time;
        public int PersonId => Comment.PersonId;
        public NotifyTaskCompletion<string> PersonNameFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> PersonImageFromId { get; private set; }

        public string Text => Comment.Text;
        public ImageSource ImageAsImageStream => Comment.ImageAsImageStream;

    }
}