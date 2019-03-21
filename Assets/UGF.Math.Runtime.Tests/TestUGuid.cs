using System;
using NUnit.Framework;
using UnityEngine;

namespace UGF.Math.Runtime.Tests
{
    public class TestUGuid
    {
        [Test]
        public void Empty()
        {
            Assert.AreEqual(Guid.Empty.ToString("N"), UGuid.Empty.ToString());
        }
        
        [Test]
        public void Ctor()
        {
            var uguid = new UGuid(0L, 15L);

            Assert.AreEqual(0L, uguid.Value0);
            Assert.AreEqual(15L, uguid.Value1);
            Assert.AreEqual("00000000000000000f00000000000000", uguid.ToString());
        }

        [Test]
        public void CtorUGuid()
        {
            var uguid = new UGuid(10L, 20L);
            var uguid2 = new UGuid(uguid);

            Assert.AreEqual(uguid, uguid2);
            Assert.AreEqual(10L, uguid2.Value0);
            Assert.AreEqual(20L, uguid2.Value1);
        }

        [Test]
        public void CtorGuid()
        {
            Guid guid = Guid.NewGuid();
            var uguid = new UGuid(guid);

            Assert.AreEqual(guid.ToString("N"), uguid.ToString());
        }

        [Test]
        public void CtorGuidString()
        {
            var uguid = new UGuid("00000000000000000f00000000000000");

            Assert.AreEqual(uguid.ToString(), "00000000000000000f00000000000000");
            Assert.AreEqual(0L, uguid.Value0);
            Assert.AreEqual(15L, uguid.Value1);
        }

        [Test]
        public void CtorBytes()
        {
            var guid = new Guid("6c41817cabb74a9491243dbf485bfd88");
            byte[] bytes = guid.ToByteArray();
            var uguid = new UGuid(bytes);
            
            Assert.AreEqual(guid.ToString("N"), uguid.ToString());
        }

        [Test]
        public void ToByteArray()
        {
            UGuid uguid = UGuid.NewUGuid();
            byte[] bytes = uguid.ToByteArray();
            var guid = new Guid(bytes);
            
            Assert.NotNull(bytes);
            Assert.Greater(bytes.Length, 0);
            Assert.AreEqual(uguid.ToString(), guid.ToString("N"));
        }
        
        [Test]
        public void Equals()
        {
            var uguid0 = new UGuid("383a095819914c549ddb7e41cc1285bb");
            var uguid1 = new UGuid("383a095819914c549ddb7e41cc1285bb");
            
            Assert.AreEqual(uguid0, uguid1);
            Assert.True(uguid0 == uguid1);
        }

        [Test]
        public void ImplicitOperatorToGuid()
        {
            var guid = new Guid("1475a7063d8748268af55387c9120db8");
            var uguid = new UGuid("1475a7063d8748268af55387c9120db8");

            Guid guid0 = uguid;
            
            Assert.AreEqual(guid, guid0);
        }

        [Test]
        public void ImplicitOperatorToUGuid()
        {
            var guid = new Guid("29bebe9136cb4e4e941c38c295d843a0");
            var uguid = new UGuid("29bebe9136cb4e4e941c38c295d843a0");

            UGuid uguid0 = guid;
            
            Assert.AreEqual(uguid, uguid0);
        }

        [Test]
        public void ToJson()
        {
            var uguid = new UGuid(0L, 15L);
            string value = JsonUtility.ToJson(uguid);
            
            Assert.AreEqual("{\"m_value0\":0,\"m_value1\":15}", value);
        }

        [Test]
        public void FromJson()
        {
            string value = "{\"m_value0\":0,\"m_value1\":15}";
            var uguid = JsonUtility.FromJson<UGuid>(value);
            
            Assert.AreEqual("00000000000000000f00000000000000", uguid.ToString());
        }
    }
}