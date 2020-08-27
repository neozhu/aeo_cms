using System;
using System.Data.Entity;
using AutoMapper;
using LazyCache;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using WebApp.Models;
using WebApp.Services;

namespace WebApp
{
  /// <summary>
  /// Specifies the Unity configuration for the main container.
  /// </summary>
  public class MvcUnityConfig
  {
    #region Unity Container
    private static  readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
      var container = new UnityContainer();
      RegisterTypes(container);
      return container;
    });
    


    /// <summary>
    /// Configured Unity Container.
    /// </summary>
    public static IUnityContainer Container => container.Value;
    /// <summary>
    /// Gets the configured Unity container.
    /// </summary>
    public static IUnityContainer GetConfiguredContainer() => container.Value;
    #endregion

    /// <summary>Registers the type mappings with the Unity container.</summary>
    /// <param name="container">The unity container to configure.</param>
    /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
    /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
    public static void RegisterTypes(IUnityContainer container)
    {
      // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
      // container.LoadConfiguration();
      // TODO: Register your types here

      //添加cache功能
      container.RegisterType<IAppCache, CachingService>(new HierarchicalLifetimeManager(), new InjectionConstructor(CachingService.DefaultCacheProvider));
      //注册Nlog功能
      container.AddNewExtension<Unity.NLog.NLogExtension>();
      //注册Database功能
      container.RegisterInstance(SqlSugarFactory.CreateSqlSugarClient(), InstanceLifetime.Singleton);
      //注册automapper
      var config = new MapperConfiguration(cfg =>
      {
        //Create all maps here
        cfg.AddProfile(new AutoMapperProfile());
      });
      container.RegisterInstance(config.CreateMapper());
      //注册EF
      container.RegisterType<DbContext, StoreContext>(new PerRequestLifetimeManager());
      container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());
      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();
      container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
      container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();
      container.RegisterType<IRepositoryAsync<Notification>, Repository<Notification>>();
      container.RegisterType<INotificationService, NotificationService>();
      container.RegisterType<ICodeItemService, CodeItemService>();
      container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();
      container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();
      container.RegisterType<IMenuItemService, MenuItemService>();
      container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();
      container.RegisterType<IRoleMenuService, RoleMenuService>();
      container.RegisterType<IRepositoryAsync<Log>, Repository<Log>>();
      container.RegisterType<ILogService, LogService>();



      //业务实体相关
      container.RegisterType<IRepositoryAsync<Company>, Repository<Company>>();
      container.RegisterType<ICompanyService, CompanyService>();
      container.RegisterType<IRepositoryAsync<Attachment>, Repository<Attachment>>();
      container.RegisterType<IAttachmentService, AttachmentService>();

      container.RegisterType<IRepositoryAsync<Category>, Repository<Category>>();
      container.RegisterType<ICategoryService, CategoryService>();
      container.RegisterType<IRepositoryAsync<ActionLog>, Repository<ActionLog>>();
      container.RegisterType<IActionLogService, ActionLogService>();
      container.RegisterType<IRepositoryAsync<ProductFile>, Repository<ProductFile>>();
      container.RegisterType<IProductFileService, ProductFileService>();
      container.RegisterType<IRepositoryAsync<ProductPricture>, Repository<ProductPricture>>();
      container.RegisterType<IProductPrictureService, ProductPrictureService>();
      container.RegisterType<IRepositoryAsync<ProductPack>, Repository<ProductPack>>();
      container.RegisterType<IProductPackService, ProductPackService>();
      container.RegisterType<IRepositoryAsync<ProductSalesHistoricalPrice>, Repository<ProductSalesHistoricalPrice>>();
      container.RegisterType<IProductSalesHistoricalPriceService, ProductSalesHistoricalPriceService>();
      container.RegisterType<IRepositoryAsync<ProductPurchaseHistoricalPrice>, Repository<ProductPurchaseHistoricalPrice>>();
      container.RegisterType<IProductPurchaseHistoricalPriceService, ProductPurchaseHistoricalPriceService>();
      container.RegisterType<IRepositoryAsync<Product>, Repository<Product>>();
      container.RegisterType<IProductService, ProductService>();
      //客户相关
      container.RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>();
      container.RegisterType<ICustomerService, CustomerService>();
      container.RegisterType<IRepositoryAsync<CustomerContact>, Repository<CustomerContact>>();
      container.RegisterType<ICustomerContactService, CustomerContactService>();
      container.RegisterType<IRepositoryAsync<CustomerBank>, Repository<CustomerBank>>();
      container.RegisterType<ICustomerBankService, CustomerBankService>();
      container.RegisterType<IRepositoryAsync<CustomerCommunication>, Repository<CustomerCommunication>>();
      container.RegisterType<ICustomerCommunicationService, CustomerCommunicationService>();
      container.RegisterType<IRepositoryAsync<CustomerFile>, Repository<CustomerFile>>();
      container.RegisterType<ICustomerFileService, CustomerFileService>();
      container.RegisterType<IRepositoryAsync<CustomerFollow>, Repository<CustomerFollow>>();
      container.RegisterType<ICustomerFollowService, CustomerFollowService>();
      container.RegisterType<IRepositoryAsync<CustomerSales>, Repository<CustomerSales>>();
      container.RegisterType<ICustomerSalesService, CustomerSalesService>();
      container.RegisterType<IRepositoryAsync<CustomerShare>, Repository<CustomerShare>>();
      container.RegisterType<ICustomerShareService, CustomerShareService>();
      container.RegisterType<IRepositoryAsync<CustomerWarehouse>, Repository<CustomerWarehouse>>();
      container.RegisterType<ICustomerWarehouseService, CustomerWarehouseService>();
      container.RegisterType<IRepositoryAsync<CustomerAttentionProduct>, Repository<CustomerAttentionProduct>>();
      container.RegisterType<ICustomerAttentionProductService, CustomerAttentionProductService>();
      container.RegisterType<IRepositoryAsync<OpportunityStage>, Repository<OpportunityStage>>();
      container.RegisterType<IOpportunityStageService, OpportunityStageService>();
      container.RegisterType<IRepositoryAsync<BusinessOpportunity>, Repository<BusinessOpportunity>>();
      container.RegisterType<IBusinessOpportunityService, BusinessOpportunityService>();
      container.RegisterType<IRepositoryAsync<MarketAct>, Repository<MarketAct>>();
      container.RegisterType<IMarketActService, MarketActService>();
      //商品编码库
      container.RegisterType<IRepositoryAsync<HSCode>, Repository<HSCode>>();
      container.RegisterType<IHSCodeService, HSCodeService>();
      //口岸代码库
      container.RegisterType<IRepositoryAsync<GPort>, Repository<GPort>>();
      container.RegisterType<IGPortService, GPortService>();
      //出口管理
      //询价任务单
      container.RegisterType<IRepositoryAsync<InquiryTaskProduct>, Repository<InquiryTaskProduct>>();
      container.RegisterType<IInquiryTaskProductService, InquiryTaskProductService>();
      container.RegisterType<IRepositoryAsync<InquiryTask>, Repository<InquiryTask>>();
      container.RegisterType<IInquiryTaskService, InquiryTaskService>();
      container.RegisterType<IRepositoryAsync<InquiryFile>, Repository<InquiryFile>>();
      container.RegisterType<IInquiryFileService, InquiryFileService>();
      container.RegisterType<IRepositoryAsync<InquiryProduct>, Repository<InquiryProduct>>();
      container.RegisterType<IInquiryProductService, InquiryProductService>();
      container.RegisterType<IRepositoryAsync<InquiryRef>, Repository<InquiryRef>>();
      container.RegisterType<IInquiryRefService, InquiryRefService>();
      container.RegisterType<IRepositoryAsync<Inquiry>, Repository<Inquiry>>();
      container.RegisterType<IInquiryService, InquiryService>();
      //报价单
      container.RegisterType<IRepositoryAsync<QuotationFile>, Repository<QuotationFile>>();
      container.RegisterType<IQuotationFileService, QuotationFileService>();
      container.RegisterType<IRepositoryAsync<QuotationProduct>, Repository<QuotationProduct>>();
      container.RegisterType<IQuotationProductService, QuotationProductService>();
      container.RegisterType<IRepositoryAsync<Quotation>, Repository<Quotation>>();
      container.RegisterType<IQuotationService, QuotationService>();
      container.RegisterType<IRepositoryAsync<QuotationCharge>, Repository<QuotationCharge>>();
      container.RegisterType<IQuotationChargeService, QuotationChargeService>();
      //AEO自测
      container.RegisterType<IRepositoryAsync<QuestionTpl>, Repository<QuestionTpl>>();
      container.RegisterType<IQuestionTplService, QuestionTplService>();
      container.RegisterType<IRepositoryAsync<AeoQuestion>, Repository<AeoQuestion>>();
      container.RegisterType<IAeoQuestionService, AeoQuestionService>();
      container.RegisterType<IRepositoryAsync<AeoAuthTest>, Repository<AeoAuthTest>>();
      container.RegisterType<IAeoAuthTestService, AeoAuthTestService>();
      ///
      container.RegisterType<IRepositoryAsync<ApproveHistory>, Repository<ApproveHistory>>();
      container.RegisterType<IApproveHistoryService, ApproveHistoryService>();
      //container.RegisterType<IRepositoryAsync<ReceiptManage>, Repository<ReceiptManage>>();
      //container.RegisterType<IReceiptManageService, ReceiptManageService>();
      //container.RegisterType<IRepositoryAsync<ReceiptManageBankFL>, Repository<ReceiptManageBankFL>>();
      //container.RegisterType<IReceiptManageBankFLService, ReceiptManageBankFLService>();
      //container.RegisterType<IRepositoryAsync<ReceiptManageCharge>, Repository<ReceiptManageCharge>>();
      //container.RegisterType<IReceiptManageChargeService, ReceiptManageChargeService>();
      //container.RegisterType<IRepositoryAsync<ReceiptManageFile>, Repository<ReceiptManageFile>>();
      //container.RegisterType<IReceiptManageFileService, ReceiptManageFileService>();
      //container.RegisterType<IRepositoryAsync<ReceiptManageProduct>, Repository<ReceiptManageProduct>>();
      //container.RegisterType<IReceiptManageProductService, ReceiptManageProductService>();
      //container.RegisterType<IRepositoryAsync<ReceiptManageRecDetail>, Repository<ReceiptManageRecDetail>>();
      //container.RegisterType<IReceiptManageRecDetailService, ReceiptManageRecDetailService>();
    }


  }
}
