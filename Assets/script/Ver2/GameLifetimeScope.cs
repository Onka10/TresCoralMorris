using VContainer;
using VContainer.Unity;
using UnityEngine;
using TresCoralMorris;

public class GameLifetimeScope : LifetimeScope
{
    // [SerializeField] private SampleView sampleView;
    [SerializeField] private PlayerInput _playerinput;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // インスタンスを注入するクラスを指定する
        builder.RegisterEntryPoint<Fhase1Manager>(Lifetime.Singleton);
        
        // SampleModelのインスタンスをISampleModelの型でDIコンテナに登録する
        // builder.Register<ISampleModel, SampleModel>(Lifetime.Singleton);
        
        // MonoBehaviorを継承しているクラスはこのようにDIコンテナに登録する
        // builder.RegisterComponentInHierarchy<SampleView>(); と記述するとヒエラルキーから探してきてくれる


        // MonoBehaviourを登録
        // MonoBehaviour someComponent;
        // builder.RegisterComponent(someComponent);
        // // シーン上のコンポーネントを登録
        // builder.RegisterComponentInHierarchy<FooBehaviour>();
        // // シーン上のコンポーネントのインターフェースを登録
        // builder.RegisterComponentInHierarchy<FooBehaviour>().AsImplementedInterfaces();
        // // Prefabをインスタンス化してそのコンポーネントを登録
        // builder.RegisterComponentInNewPrefab(somePrefab, Lifetime.Scoped);
        // // Prefabを親を指定してインスタンス化してそのコンポーネントを登録
        // builder.RegisterComponentInNewPrefab(somePrefab, Lifetime.Scoped).UnderTransform(parentTransform);
        // builder.RegisterComponentInNewPrefab(somePrefab, Lifetime.Scoped).UnderTransform(() => parentTransform);
        // // GameObjectをインスタンス化してそのコンポーネントを登録
        // builder.RegisterComponentOnNewGameObject<FooBehaviour>(Lifetime.Scoped, "ObjectName");
        builder.RegisterComponent(_playerinput);
    }
}