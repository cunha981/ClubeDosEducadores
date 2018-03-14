using System.Linq;
using Model;
using Helper.CacheHelper;
using System.Collections.Generic;

namespace Domain
{
    public class PerguntaFrequenteDomain : ModelDomain<PerguntaFrequente>
    {
        public IEnumerable<PerguntaFrequente> GetCache()
        {
            return _cacheManager.Get($"perguntasFrequentes", 600, () =>
            {
                return Get().ToArray();
            });
        }

        public PerguntaFrequente GetCache(int id)
        {
            return GetCache().Single(a => a.Id == id);
        }

        public IEnumerable<PerguntaFrequente> Search(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return GetCache();
            }

            var points = text.Split(' ');
            return GetCache().Where(a => points.Any(b => a.Tags.Contains(b)));
        }
    }
}
