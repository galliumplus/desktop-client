/*using GalliumPlus.WebApi.Core.Applications;
using GalliumPlus.WebApi.Core.Data;
using GalliumPlus.WebApi.Core.Exceptions;
using GalliumPlus.WebApi.Core.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GalliumPlus.WebApi.Dto
{
    public class ClientDetails
    {
        public enum ClientType { CLIENT, BOT_CLIENT, SSO_CLIENT }

        public ClientType? Type { get; set; }
        public int Id { get; set; }
        public string? ApiKey { get; set; }
        [Required] public string Name { get; set; }
        [Required] public uint? PermissionsGranted { get; set; }
        [Required] public bool? IsEnabled { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? PermissionsRevoked { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? UsesApi { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RedirectUrl { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LogoUrl { get; set; }

        public ClientDetails()
        {
            this.Id = -1;
            this.Name = String.Empty;
        }

        public class Mapper : Mapper<Client, ClientDetails>
        {
            public override ClientDetails FromModel(Client model)
            {
                ClientDetails dto = new()
                {
                    Id = model.Id,
                    ApiKey = model.ApiKey,
                    Name = model.Name,
                    PermissionsGranted = (uint)model.Granted,
                    IsEnabled = model.IsEnabled,
                };

                if (model is BotClient)
                {
                    dto.Type = ClientType.BOT_CLIENT;
                }
                else
                {
                    dto.PermissionsRevoked = (uint)model.Revoked;

                    if (model is SsoClient ssoClient)
                    {
                        dto.Type = ClientType.SSO_CLIENT;
                        dto.RedirectUrl = ssoClient.RedirectUrl;
                        dto.LogoUrl = ssoClient.LogoUrl;
                        dto.UsesApi = ssoClient.UsesApi;
                    }
                    else
                    {
                        dto.Type = ClientType.CLIENT;
                    }
                }

                return dto;
            }

            private static Exception MissingField()
            {
                return new InvalidItemException($"Les informations sur l'application sont incomplètes.");
            }

            public override Client ToModel(ClientDetails dto)
            {
                if (!dto.Type.HasValue)
                {
                    throw MissingField();
                }

                switch (dto.Type.Value)
                {
                    case ClientType.CLIENT:
                        return new Client(
                            name: dto.Name,
                            isEnabled: dto.IsEnabled!.Value,
                            granted: (Permissions)dto.PermissionsGranted!.Value,
                            revoked: (Permissions)(dto.PermissionsRevoked ?? throw MissingField())
                        );

                    case ClientType.BOT_CLIENT:
                        return new BotClient(
                            name: dto.Name,
                            isEnabled: dto.IsEnabled!.Value,
                            permissions: (Permissions)dto.PermissionsGranted!.Value
                        );

                    case ClientType.SSO_CLIENT:
                        return new SsoClient(
                            name: dto.Name,
                            isEnabled: dto.IsEnabled!.Value,
                            granted: (Permissions)dto.PermissionsGranted!.Value,
                            revoked: (Permissions)(dto.PermissionsRevoked ?? throw MissingField()),
                            usesApi: dto.UsesApi ?? throw MissingField(),
                            redirectUrl: dto.RedirectUrl ?? throw MissingField(),
                            logoUrl: dto.LogoUrl
                        );

                    default:
                        throw new InvalidItemException("Type de client invalide");
                }
            }

            public void PatchModel(Client original, ClientDetails patch)
            {
                original.Name = patch.Name;
                original.IsEnabled = patch.IsEnabled!.Value;
                original.Granted = (Permissions)patch.PermissionsGranted!.Value;

                if (original is not BotClient)
                {
                    original.Revoked = (Permissions)(patch.PermissionsRevoked ?? throw MissingField());

                    if (original is SsoClient originalSso)
                    {
                        originalSso.RedirectUrl = patch.RedirectUrl ?? throw MissingField();
                        originalSso.UsesApi = patch.UsesApi ?? throw MissingField();
                        originalSso.LogoUrl = patch.LogoUrl;
                    }
                }
            }
        }
    }
}
*/