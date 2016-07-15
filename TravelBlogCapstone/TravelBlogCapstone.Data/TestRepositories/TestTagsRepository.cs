using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.TestRepositories
{
    public class TestTagsRepository : ITagsRepository
    {
        private static List<Tag> _tags = new List<Tag>
        {
            new Tag {TagName = "africa"},
            new Tag {TagName = "beach"},
            new Tag {TagName = "boring"},
            new Tag {TagName = "brazil"},
            new Tag {TagName = "cheap"},
            new Tag {TagName = "cold"},
            new Tag {TagName = "expensive"},
            new Tag {TagName = "fun"},
            new Tag {TagName = "hot"},
            new Tag {TagName = "love"}
        };

        public List<Tag> GetAll()
        {
            return _tags;
        }

        public Tag Get(string tagName)
        {
            return _tags.FirstOrDefault(m => m.TagName == tagName);
        }

        public Tag Insert(Tag tag)
        {
            _tags.Add(tag);
            return tag;
        }

        public void Update(Tag tag)
        {
            var updateTag = _tags.FirstOrDefault(m => m.TagName == tag.TagName);
            updateTag.TagName = tag.TagName;
        }

        public void Delete(string tagName)
        {
            var removeTag = _tags.FirstOrDefault(m => m.TagName == tagName);
            _tags.Remove(removeTag);
        }
    }
}
