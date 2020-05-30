using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using InventoryControl.Controllers;
using InventoryControl.DAL;
using InventoryControl.Models.InventoryItems;
using InventoryControl.Tests.MockData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InventoryControl.Tests.Controllers
{
	[TestClass]
	public class InventoryPartsControllerTest
	{
		#region Index
		[TestMethod]
		public void TestIndex_whenItemsExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller 
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Index("name_desc", "", "", null) as ViewResult;
			Assert.IsNotNull(result);
			Assert.AreEqual(1, ((List<InventoryPart>) result.Model).Count);
		}

		[TestMethod]
		public void TestIndex_whenItemsNotExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>();
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Index("name_desc", "", "", null) as ViewResult;
			Assert.IsNotNull(result);
			Assert.AreEqual(0, ((List<InventoryPart>) result.Model).Count);
		}
		#endregion

		#region Details
		[TestMethod]
		public void TestDetails_WhenItemExists()
		{
			InventoryPart expectedObject = MockObjectsUtil.GetMockInventoryPart(1);
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { expectedObject };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Details(1) as ViewResult;
			Assert.IsNotNull(result);
			InventoryPart actualObject = (InventoryPart)result.Model;
			Assert.AreEqual(expectedObject.ID, actualObject.ID);
		}

		[TestMethod]
		public void TestDetails_WhenItemNotExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Details(2) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDetails_WhenIdIsNull()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;
			Assert.AreEqual(400, result.StatusCode);
		}
		#endregion

		#region Create
		[TestMethod]
		public void TestCreate_EmptyParameter()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Create() as ViewResult;
			Assert.IsNotNull(result);
			Assert.IsNull(result.Model);
		}

		[TestMethod]
		public void TestCreate_WithParameters()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPart inputObject = new InventoryPart()
			{
				ID = 2,
				Name = "test 2",
				ReorderLevel = 50,
				AvailabeNoOfUnits = 100,
				UnitPrice = 10
			};
			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Create(inputObject) as ViewResult;
			Assert.IsNull(result);
		}
		#endregion

		#region Edit
		[TestMethod]
		public void TestEdit_WhenItemExists()
		{
			InventoryPart expectedObject = MockObjectsUtil.GetMockInventoryPart(1);
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { expectedObject };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Edit(1) as ViewResult;
			Assert.IsNotNull(result);
			InventoryPart actualObject = (InventoryPart) result.Model;
			Assert.AreEqual(expectedObject.ID, actualObject.ID);
		}

		[TestMethod]
		public void TestEdit_WhenItemNotExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Edit(2) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestEdit_WhenIdIsNull()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			HttpStatusCodeResult result = controller.Edit((int?)null) as HttpStatusCodeResult;
			Assert.AreEqual(400, result.StatusCode);
		}

		[TestMethod]
		public void TestEdit_WithParameter()
		{
			InventoryPart inputObject = new InventoryPart()
			{
				ID = 2,
				Name = "test 2",
				ReorderLevel = 50,
				AvailabeNoOfUnits = 100,
				UnitPrice = 10
			};

			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Edit(inputObject) as ViewResult;
			Assert.IsNull(result);
		}
		#endregion

		#region Delete
		[TestMethod]
		public void TestDelete_WhenItemExists()
		{
			InventoryPart expectedObject = MockObjectsUtil.GetMockInventoryPart(1);
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { expectedObject };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Delete(1) as ViewResult;
			Assert.IsNotNull(result);
			InventoryPart actualObject = (InventoryPart) result.Model;
			Assert.AreEqual(expectedObject.ID, actualObject.ID);
		}

		[TestMethod]
		public void TestDelete_WhenItemNotExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.Delete(2) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDelete_WhenIdIsNull()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Provider)
				.Returns(data.AsQueryable().Provider);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.Expression)
				.Returns(data.AsQueryable().Expression);
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.ElementType)
				.Returns(data.AsQueryable().ElementType);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			HttpStatusCodeResult result = controller.Delete((int?) null) as HttpStatusCodeResult;
			Assert.AreEqual(400, result.StatusCode);
		}
		#endregion

		#region DeleteConfirmed
		[TestMethod]
		public void TestDeleteConfirmed_ItemExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.DeleteConfirmed(1) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDeleteConfirmed_ItemDoesNotExists()
		{
			Mock<InventoryContext> mockDbContext = new Mock<InventoryContext>();
			List<InventoryPart> data = new List<InventoryPart>() { MockObjectsUtil.GetMockInventoryPart(1) };
			Mock<DbSet<InventoryPart>> mockSet = new Mock<DbSet<InventoryPart>>();
			mockSet.As<IQueryable<InventoryPart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockDbContext.Setup(x => x.Set<InventoryPart>()).Returns(mockSet.Object);
			mockDbContext.Setup(c => c.InventoryParts).Returns(mockSet.Object);
			mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
				.Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int) ids[0]));

			InventoryPartsController controller
				= new InventoryPartsController(mockDbContext.Object);
			ViewResult result = controller.DeleteConfirmed(2) as ViewResult;
			Assert.IsNull(result);
		}
		#endregion
	}
}
