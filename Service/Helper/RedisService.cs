using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;
using System.Text.Json;
using Service.Model;

namespace Service.Helper
{

    public class RedisService
    {
        private readonly IDatabase _db;

        public RedisService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            _db = redis.GetDatabase();
        }

        public void SaveUserSession(UserSession session)
        {
            //todo desharcodear, no me funcionan las cookies
            var json = JsonSerializer.Serialize(session);
            _db.StringSet($"session:1", json, TimeSpan.FromHours(24));
        }

        public UserSession GetUserSession()
        {
            //todo desharcodear, no me funcionan las cookies
            var json = _db.StringGet($"session:1");
            return json.HasValue ? JsonSerializer.Deserialize<UserSession>(json) : null;
        }

        public void UpdateLogout()
        {
            var session = GetUserSession();
            if (session != null)
            {
                session.Logout = DateTime.Now;
                SaveUserSession(session);
            }
        }

        public TimeSpan? GetConnectionDuration()
        {
            var session = GetUserSession();
            if (session?.Logout != null)
            {
                return session.Logout.Value - session.Login;
            }
            return null;
        }

        public string CategorizeUser()
        {
            var duration = GetConnectionDuration();
            if (duration == null) return "UNKNOWN";

            if (duration.Value.TotalMinutes > 240)
                return usuarioCat.TOP.ToString();
            if (duration.Value.TotalMinutes >= 120)
                return usuarioCat.MED.ToString();
            return usuarioCat.LOW.ToString();
        }

        enum usuarioCat
        {
            LOW,
            MED,
            TOP
        }
        //CARRITO
        public void SaveCarritoSession(CarritoSession session)
        {
            //todo desharcodear, no me funcionan las cookies
            var json = JsonSerializer.Serialize(session);
            _db.StringSet($"carrito:1", json, TimeSpan.FromHours(24));
        }
        public List<CarritoItemSession> GetCarritoItems(int userId)
        {
            var json = _db.StringGet(GetCartKey(userId));
            return json.HasValue
                ? JsonSerializer.Deserialize<List<CarritoItemSession>>(json)
                : new List<CarritoItemSession>();
        }

        private string GetCartKey(int userId) { return $"carrito:1"; } //desharcodear con la cookie


        public void AddOrUpdateItem(int userId, CarritoItemSession newItem)
        {
            var carrito = GetCarritoItems(userId);
            var existing = carrito.FirstOrDefault(x => x.IdItem == newItem.IdItem);

            if (existing != null)
            {
                existing.Cantidad = newItem.Cantidad;
            }
            else
            {
                carrito.Add(newItem);
            }

            _db.StringSet(GetCartKey(userId), JsonSerializer.Serialize(carrito));
        }
        public void ClearCarrito(int userId)
        {
            _db.KeyDelete(GetCartKey(userId));
        }

        public void RemoveItem(int userId, int idItem)
        {
            var carrito = GetCarritoItems(userId);
            carrito.RemoveAll(x => x.IdItem == idItem);
            _db.StringSet(GetCartKey(userId), JsonSerializer.Serialize(carrito));
        }
    }
}