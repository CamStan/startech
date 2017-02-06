using FakeNewsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeNewsProject.ViewModels
{
    /// <summary>
    /// Viewmodel to hold a story and a list of viewmodels associating a boolean to each tag
    /// </summary>
    public class TagStories
    {
        public Story TheStory { get; set; }
        public List<TagSelect> TheTags { get; set; }
    }
}