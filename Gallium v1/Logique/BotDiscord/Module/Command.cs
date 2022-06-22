using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using System.Threading.Tasks;

namespace BotETIQ.Module
{
    public class Command : ModuleBase<SocketCommandContext>
    {
        private bool etiqStatus = false;
        private bool alreadyOpen = false;

        /// <summary>
        /// Commande discord
        /// </summary>
        /// <returns></returns>
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("pong");
        }

        /// <summary>
        /// renvoie la photo de profil de l'utilisateur ainsi que son pseudo
        /// </summary>
        /// <param name="size"> taille de la photo </param>
        [Command("avatar")]
        public async Task AvatarAsync(ushort size = 512)
        {
            await ReplyAsync(Context.User.ToString());
            await ReplyAsync(CDN.GetUserAvatarUrl(Context.User.Id, Context.User.AvatarId, size, ImageFormat.Auto));
        }

        /// <summary>
        /// Ouvre le discord de l'ETIQ
        /// </summary>
        /// <returns></returns>
        [Command("Open")]
        public async Task EtiqOpen()
        {
            
            alreadyOpen = true;
            var Builder = new EmbedBuilder()
            {
                Color = Color.DarkerGrey,
                Title = "L'ETIQ est ouverte !",
                Description = "Viens à l'ETIQ !",
                ThumbnailUrl = "https://zupimages.net/up/22/11/z8io.png"
            };

            var msg = await ReplyAsync("", false, Builder.Build());    
        }
        
        
        /// <summary>
        /// Ferme le discord de l'ETIQ
        /// </summary>
        /// <returns></returns>
        [Command("Close")]
        public async Task EtiqClose()
        {
            
            alreadyOpen = false;
            var Builder = new EmbedBuilder()
            {
                Color = Color.DarkerGrey,
                Title = "L'ETIQ est fermé !",
                Description = "C'est fermé ... ",
                ThumbnailUrl = "https://zupimages.net/up/22/11/f6fi.png"
            };

            var msg = await ReplyAsync("", false, Builder.Build());    
        }

    }
}
