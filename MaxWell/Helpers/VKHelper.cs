using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using MaxWell.Models;

namespace MaxWell.Helpers
{
    class VKHelper
    {
       

        public static Person ProfileToPerson(LoginResult UserProfile)
        {
            //"id,first_name,last_name,sex,bdate,city,country,photo_50,photo_100,photo_200_orig,photo_200,photo_400_orig,photo_max,photo_max_orig,online,online_mobile,lists,domain,has_mobile,contacts,connections,site,education,universities,schools,can_post,can_see_all_posts,can_see_audio,can_write_private_message,status,last_seen,common_count,relation,relatives,counters"
            Person person = new Person();
            person.Name = UserProfile.FirstName + " " + UserProfile.LastName;
           
            person.ImageUrl = UserProfile.ImageUrl;
            
            person.Email = UserProfile.Email;
            person.Description = "";
            person.VK = "";
            person.Cats = "";
            person.Gender = UserProfile.Gender;
            person.Pitomniki = "";
            person.Phone = UserProfile.Phone;
         //   person.image = null;
         //   person.icon = new byte[] {0 };
            try
            {
                if (person.image == null && (person.ImageUrl != null))
                {
                    person.image = ImageHelper.ImageUrlToByteArray(person.ImageUrl);
                }

            }
            catch (Exception e)
            {
           //   await   UserDialogs.Instance.AlertAsync(this.GetType() + " Error Date or Image transformation", e.Message);
            }
            try
            {
        
                person.Birthday = DateTime.ParseExact(UserProfile.Birthday, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            }
            catch (Exception e)
            {
                //   await   UserDialogs.Instance.AlertAsync(this.GetType() + " Error Date or Image transformation", e.Message);
            }
            person.VKUserId = UserProfile.UserId;
               //   person.image = 
            return person;

        }
    }
}
