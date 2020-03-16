using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Models;

namespace testproj.Services
{
    public class SliderService
    {
        private readonly IMongoCollection<Slider> _sliders;

        public SliderService(IConfiguration config)
        {
            var url = MongoUrl.Create(config.GetConnectionString("StoreDb"));
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            _sliders = db.GetCollection<Slider>("slider");
        }

        public List<Slider> Get()
        {
            return _sliders.Find(slider => true).ToList();
        }

        public Slider Get(string id)
        {
            return _sliders.Find(slider => slider.Id == id).FirstOrDefault();
        }

        public Slider Create(Slider slider)
        {
            slider.Id = ObjectId.GenerateNewId().ToString();
            _sliders.InsertOne(slider);
            return slider;
        }

        public void Update(string id, Slider sliderIn)
        {
            _sliders.ReplaceOne(slider => slider.Id == id, sliderIn);
        }

        public void Remove(string id)
        {
            _sliders.DeleteOne(slider => slider.Id == id);
        }
    }
}
