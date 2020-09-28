using System.Collections.Generic;
using NUnit.Framework;

namespace TomLonghurst.UrlShortener.Site.UnitTests
{
    public class KeyGeneratorTests
    {
        private KeyGenerator _keyGenerator;
        private readonly List<string> _previouslyGeneratedKeys = new List<string>();

        [SetUp]
        public void Setup()
        {
            _keyGenerator = new KeyGenerator();
        }

        [Repeat(1000)]
        [Test]
        public void KeyIsUnique()
        {
            var key = _keyGenerator.GenerateUniqueKey();
            
            Assert.That(key, Is.Not.Null.Or.Empty);
            Assert.That(key, Is.TypeOf<string>());
            Assert.That(key.Length, Is.EqualTo(6));
            
            Assert.That(_previouslyGeneratedKeys, Does.Not.Contain(key));
            
            _previouslyGeneratedKeys.Add(key);
        }
    }
}