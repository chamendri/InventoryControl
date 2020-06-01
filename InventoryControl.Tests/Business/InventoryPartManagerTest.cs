using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InventoryControl.Business.InventoryItem;
using InventoryControl.Common.ViewModels.InventoryItems;
using InventoryControl.DAL;
using InventoryControl.Models.InventoryItems;
using InventoryControl.Tests.MockData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InventoryControl.Tests.Business
{
	[TestClass]
	public class InventoryPartManagerTest
	{
		[TestMethod]
		public void GetInventoryPartsTest_WhenItemsExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPartDto> data = new List<InventoryPartDto>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPartDto>> mockSet = new Mock<DbSet<InventoryPartDto>>();
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);

			mockDbContext.Setup(x => x.Set<InventoryPartDto>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartManager manager = new InventoryPartManager(mockDbContext.Object);
			IEnumerable<InventoryPartView> viewObects = manager.GetInventoryParts("name_desc", "");
			Assert.IsNotNull(viewObects);
			Assert.AreEqual(1, viewObects.Count());
		}

		[TestMethod]
		public void GetInventoryPartsTest_WhenItemsNotExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPartDto> data = new List<InventoryPartDto>() {  };
			Mock<DbSet<InventoryPartDto>> mockSet = new Mock<DbSet<InventoryPartDto>>();
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);

			mockDbContext.Setup(x => x.Set<InventoryPartDto>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartManager manager = new InventoryPartManager(mockDbContext.Object);
			IEnumerable<InventoryPartView> viewObects = manager.GetInventoryParts("name_desc", "");
			Assert.IsNotNull(viewObects);
			Assert.AreEqual(0, viewObects.Count());
		}

		[TestMethod]
		public void GetInventoryPartFromId_WhenIdExists()
		{
			InventoryPartDto expectedObject = MockObjectsUtil.GetMockInventoryPart(1);
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPartDto> data = new List<InventoryPartDto>() { expectedObject };
			Mock<DbSet<InventoryPartDto>> mockSet = new Mock<DbSet<InventoryPartDto>>();
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPartDto>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartManager manager = new InventoryPartManager(mockDbContext.Object);
			InventoryPartView result = manager.GetInventoryPartFromId(1);
			Assert.AreEqual(expectedObject.ID, result.ID);
		}

		[TestMethod]
		public void GetInventoryPartFromId_WhenIdNotExists()
		{
			InventoryPartDto expectedObject = MockObjectsUtil.GetMockInventoryPart(1);
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPartDto> data = new List<InventoryPartDto>() { expectedObject };
			Mock<DbSet<InventoryPartDto>> mockSet = new Mock<DbSet<InventoryPartDto>>();
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPartDto>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartManager manager = new InventoryPartManager(mockDbContext.Object);
			InventoryPartView result = manager.GetInventoryPartFromId(2);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void InsertInventoryPart_WhenInputExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPartDto> data = new List<InventoryPartDto>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPartDto>> mockSet = new Mock<DbSet<InventoryPartDto>>();
			mockSet.As<IQueryable<InventoryPartDto>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPartDto>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

		}
	}
}
