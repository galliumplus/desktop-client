using GalliumPlusApi.CompatibilityHelpers;
using Modeles;
using Org.BouncyCastle.Asn1.Crmf;
using System.Text.Json.Serialization;

namespace GalliumPlusApi.Dto
{
    public class HistoryActionDetails
    {
        public string ActionKind { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime Time { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Actor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? NumericValue { get; set; }

        public class Mapper : Mapper<Log?, HistoryActionDetails>
        {
            public override HistoryActionDetails FromModel(Log? model)
            {
                throw new InvalidOperationException("Les données de l'historique ne peuvent pas sortir.");
            }

            public override Log? ToModel(HistoryActionDetails dto)
            {
                if (LogThemeMapper.ActionKindToLogTheme(dto.ActionKind) is int theme)
                {
                    return new Log(
                        date: dto.Time,
                        theme: theme,
                        message: dto.Text,
                        auteur: dto.Actor ?? "Personne"
                    );
                }
                else
                {
                    return null;
                }
            }

            public new IEnumerable<Log> ToModel(IEnumerable<HistoryActionDetails> dtos)
            {
                foreach (var dto in dtos)
                {
                    if (this.ToModel(dto) is Log log)
                    {
                        yield return log;
                    }
                }
                yield break;
            }
        }
    }
}
