using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyingTicketsOnline
{
    internal class CurrentUser
    {
        private string UserId { get; set; }
        private string Name { get; set; }
        private string Surname { get; set; }
        private int UserBalanse { get; set; }
        private string CardId { get; set; }
        private string Expity { get; set; }
        private int CardCvv { get; set; }

        public CurrentUser(string UserId, string Name, string Surname, string CardId, string Expity,int CardCvv) 
        {
            UserId = this.UserId; 
            Name = this.Name; 
            Surname = this.Surname; 
            CardId = this.CardId;
            Expity = this.Expity;
            CardCvv = this.CardCvv;
            UserBalanse = 0;
        }

    }
}
