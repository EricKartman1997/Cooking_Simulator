using Zenject;

public class DialogueManager
{
    private EndDialogueUI _endDialogue;
    private StartDialogueUI _startDialogue;
    private TaskDialogueUI _taskDialogue;
    private MiniTaskDialogueUI _miniTaskDialogue;
    
    private GetTableTutorialDecorator _getTableApple;
    private GetTableTutorialDecorator _getTableOrange;
    private GiveTableTutorialDecorator _giveTable;
    private CuttingTableTutorialDecorator _cuttingTable;
    private DistributionTutorialDecorator _distribution;
    
    private FactoryUIGameplay _factoryUIGameplay;
    private OrdersService _ordersService;
    private readonly IInputBlocker _inputBlocker;
    private ChecksManager _checksManager;

    
    [Inject]
    public DialogueManager(OrdersService ordersService, IInputBlocker inputBlocker,ChecksManager checksManager)
    {
        _ordersService = ordersService;
        _inputBlocker = inputBlocker;
        _checksManager = checksManager;
    }
    
    private void StartWedding()
    {
        DisableMovement();
        _startDialogue.Show();
        _startDialogue.Button.onClick.AddListener(StartCookingSalat);
    }
    
    private void StartCookingSalat()
    {
        _startDialogue.Button.onClick.RemoveListener(StartCookingSalat);
        _taskDialogue.Show();
        _taskDialogue.Button.onClick.AddListener(StartTakeApple);
    }
    
    // выдали задание взять яблоко
    private void StartTakeApple()
    {
        _taskDialogue.Button.onClick.RemoveListener(StartTakeApple);
        _miniTaskDialogue.Show();
        _getTableApple.StartBlink();
        _miniTaskDialogue.Button.onClick.AddListener(_factoryUIGameplay.ShowOrder);// вкл заказы 
        _miniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
    }
    
    //взял яблоко
    private void StartTellGarbage()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(_factoryUIGameplay.ShowOrder);// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        
        _getTableApple.StopBlink();
        DisableMovement(); // выкл управление
        _miniTaskDialogue.ChangeText(DialogueText.GARBAGE_TITLE,DialogueText.GARBAGE_DESCRIPTION);// меняем текст (рассказать про мусорку что можно выкинуть предмет)
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener(StartBringCuttingTable);
    }
    
    // рассказали про мусорку
    private void StartBringCuttingTable() 
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(StartBringCuttingTable);// отписка
        
        _miniTaskDialogue.ChangeText(DialogueText.CUTTING_TABLE_TITLE,DialogueText.CUTTING_TABLE_DESCRIPTION);// меняем текст (задание положить предмет на разделочный стол)
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        _miniTaskDialogue.Button.onClick.AddListener(_cuttingTable.StartBlink);// вкл мигание
        
    }
    
    // положил яблоко на разделочный стол
    private void StartTakeOrange()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(_cuttingTable.StartBlink);// отписка

        _cuttingTable.StopBlink();
        DisableMovement(); // выкл управление
        _miniTaskDialogue.ChangeText(DialogueText.TAKE_ORANGE_TITLE,DialogueText.TAKE_ORANGE_DESCRIPTION);// меняем текст (взять апельсин)
        _miniTaskDialogue.Show();

        _miniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        _miniTaskDialogue.Button.onClick.AddListener(_getTableOrange.StartBlink);// вкл мигание
    }

    // взял апельсин
    private void StartSecondBringCuttingTable()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(_getTableOrange.StartBlink);// отписка
        
        _cuttingTable.StopBlink();
        DisableMovement(); // выкл управление
        _miniTaskDialogue.ChangeText(DialogueText.SECOND_CUTTING_TABLE_TITLE,DialogueText.SECOND_CUTTING_TABLE_DESCRIPTION);// меняем текст ()
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
    }
    
    // салат был приготовлен
    private void StartCompliment()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        
        DisableMovement(); // выкл управление
        _taskDialogue.ChangeText(DialogueText.COMPLIMENT_TITLE,DialogueText.COMPLIMENT_DESCRIPTION); // меняем текст ()
        _taskDialogue.Show();
        _taskDialogue.Button.onClick.AddListener(StartWaitOrder);
    }
    
    private void StartWaitOrder()
    {
        _taskDialogue.Button.onClick.RemoveListener(StartWaitOrder);// отписка
        
        _miniTaskDialogue.ChangeText(DialogueText.WAIT_ORDER_TITLE,DialogueText.WAIT_ORDER_DESCRIPTION);// меняем текст ()
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        _miniTaskDialogue.Button.onClick.AddListener(_checksManager.AddCheckTutorial);// добавить чек
        _miniTaskDialogue.Button.onClick.AddListener(_distribution.StartBlink);// вкл мигание
    }
    
    // чек появился
    // заказ на Distribution
    // анимация закончена
    
    private void StartGoodbye()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(_checksManager.AddCheckTutorial);// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(_distribution.StartBlink);// отписка
        _endDialogue.Show();
    }
    
    private void DisableMovement()
    {
        _inputBlocker.Block(this, InputBlockType.Movement);
    }

    private void EnableMovement()
    {
        _inputBlocker.Unblock(this, InputBlockType.Movement);
    }


}
