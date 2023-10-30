using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalliumPlusApi.CompatibilityHelpers
{
    internal class UserIdRepository
    {
        private static UserIdRepository? current;

        public static UserIdRepository Current
        {
            get
            {
                if (current == null)
                {
                    current = new UserIdRepository();
                }
                return current;
            }
        }

        private Dictionary<string, int> ids;
        private int nextAutoId;

        private UserIdRepository()
        {
            ids = new Dictionary<string, int>();
            nextAutoId = 1;
        }

        public int GetIdFor(string username)
        {
            lock (ids)
            {
                if (ids.ContainsKey(username))
                {
                    return ids[username];
                }
                else
                {
                    int id = nextAutoId++;
                    ids[username] = id;
                    return id;
                }
            }
        }

        public string FindUsernameOf(int numericId)
        {
            lock (ids)
            {
                return ids.First(kvp => kvp.Value == numericId).Key;
            }
        }
    }
}
