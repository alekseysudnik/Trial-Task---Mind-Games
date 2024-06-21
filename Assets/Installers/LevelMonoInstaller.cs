using UnityEngine;
using Zenject;

public class LevelMonoInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ItemsFactory itemsFactory;
    [SerializeField] private LevelManager levelManager;
    public override void InstallBindings()
    {
        Container.Bind<ItemsFactory>().FromInstance(itemsFactory).AsSingle();
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle();
        var playerInstance = Container.InstantiatePrefabForComponent<Player>(player, spawnPoint.position, Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(playerInstance).AsSingle().NonLazy();
        Container.QueueForInject(playerInstance);
    }
}