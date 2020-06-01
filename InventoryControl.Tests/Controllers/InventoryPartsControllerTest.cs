using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using InventoryControl.Business.InventoryItem;
using InventoryControl.Common.ViewModels.InventoryItems;
using InventoryControl.Controllers;
using InventoryControl.DAL;
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
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.GetInventoryParts(It.IsAny<string>(), It.IsAny<string>())).Returns(data);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Index("name_desc", "", "", null) as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestIndex_whenItemsNotExists()
		{
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.GetInventoryParts(It.IsAny<string>(), It.IsAny<string>())).Returns(data);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Index("name_desc", "", "", null) as ViewResult;
			Assert.IsNotNull(result);
		}
		#endregion

		#region Details
		[TestMethod]
		public void TestDetails_WhenItemExists()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.GetInventoryPartFromId(It.IsAny<int?>())).Returns(expectedObject);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Details(1) as ViewResult;
			Assert.IsNotNull(result);
			InventoryPartView actualObject = (InventoryPartView) result.Model;
			Assert.AreEqual(expectedObject.ID, actualObject.ID);
		}

		[TestMethod]
		public void TestDetails_WhenItemNotExists()
		{
			InventoryPartView expectedObject = null;
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.GetInventoryPartFromId(It.IsAny<int?>())).Returns(expectedObject);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Details(2) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDetails_WhenIdIsNull()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.GetInventoryPartFromId(It.IsAny<int?>())).Returns(expectedObject);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;
			Assert.AreEqual(400, result.StatusCode);
		}
		#endregion

		#region Create
		[TestMethod]
		public void TestCreate_EmptyParameter()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.InsertInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Create() as ViewResult;
			Assert.IsNotNull(result);
			Assert.IsNull(result.Model);
		}

		[TestMethod]
		public void TestCreate_WithParameters()
		{
			InventoryPartView inputObject = new InventoryPartView()
			{
				ID = 2,
				Name = "test 2",
				ReorderLevel = 50,
				AvailabeNoOfUnits = 100,
				UnitPrice = 10
			};
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.InsertInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Create(inputObject) as ViewResult;
			Assert.IsNull(result);
		}
		#endregion

		#region Edit
		[TestMethod]
		public void TestEdit_WhenItemExists()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.UpdateInventoryPart(It.IsAny<InventoryPartView>()));
			mockManager.Setup(x => x.GetInventoryPartFromId(It.IsAny<int?>())).Returns(expectedObject);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Edit(1) as ViewResult;
			Assert.IsNotNull(result);
			InventoryPartView actualObject = (InventoryPartView) result.Model;
			Assert.AreEqual(expectedObject.ID, actualObject.ID);
		}

		[TestMethod]
		public void TestEdit_WhenItemNotExists()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.UpdateInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Edit(2) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestEdit_WhenIdIsNull()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.UpdateInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			HttpStatusCodeResult result = controller.Edit((int?) null) as HttpStatusCodeResult;
			Assert.AreEqual(400, result.StatusCode);
		}

		[TestMethod]
		public void TestEdit_WithParameter()
		{
			InventoryPartView inputObject = new InventoryPartView()
			{
				ID = 2,
				Name = "test 2",
				ReorderLevel = 50,
				AvailabeNoOfUnits = 100,
				UnitPrice = 10
			};

			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.UpdateInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Edit(inputObject) as ViewResult;
			Assert.IsNull(result);
		}
		#endregion

		#region Delete
		[TestMethod]
		public void TestDelete_WhenItemExists()
		{
			InventoryPartView expectedObject = MockObjectsUtil.GetMockInventoryPartView(1);
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.DeleteInventoryPart(It.IsAny<InventoryPartView>()));
			mockManager.Setup(x => x.GetInventoryPartFromId(It.IsAny<int?>())).Returns(expectedObject);
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Delete(1) as ViewResult;
			Assert.IsNotNull(result);
			InventoryPartView actualObject = (InventoryPartView) result.Model;
			Assert.AreEqual(expectedObject.ID, actualObject.ID);
		}

		[TestMethod]
		public void TestDelete_WhenItemNotExists()
		{
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.DeleteInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.Delete(2) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDelete_WhenIdIsNull()
		{
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.DeleteInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			HttpStatusCodeResult result = controller.Delete((int?) null) as HttpStatusCodeResult;
			Assert.AreEqual(400, result.StatusCode);
		}
		#endregion

		#region DeleteConfirmed
		[TestMethod]
		public void TestDeleteConfirmed_ItemExists()
		{
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.DeleteInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.DeleteConfirmed(1) as ViewResult;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDeleteConfirmed_ItemDoesNotExists()
		{
			List<InventoryPartView> data = new List<InventoryPartView>() { MockObjectsUtil.GetMockInventoryPartView(1) };
			Mock<InventoryPartManager> mockManager = new Mock<InventoryPartManager>();
			mockManager.Setup(x => x.DeleteInventoryPart(It.IsAny<InventoryPartView>()));
			InventoryPartsController controller = new InventoryPartsController(mockManager.Object);
			ViewResult result = controller.DeleteConfirmed(2) as ViewResult;
			Assert.IsNull(result);
		}
		#endregion
	}
}
