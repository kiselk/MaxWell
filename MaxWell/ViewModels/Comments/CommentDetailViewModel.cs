using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MaxWell.Models;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Comments
{
	public class CommentDetailViewModel : BaseViewModel
    {

        public CommentDetailViewModel()
        {
        }

        public CommentDetailViewModel(Comment vyazka)
        {
            this.Comment = vyazka;


        }



        private Comment _vyazka;

        public Comment Comment
        {
            get => _vyazka;
            set { SetProperty(ref _vyazka, value); }
        }
    }
}