public class CubeSpawnerView : SpawnerView<Cube>
{
    protected override void UpdateActive(int count)
    {
        ActiveText.text = $"Количество активных кубов на сцене: {count}";
    }

    protected override void UpdateCreated(int count)
    {
        CreatedText.text = $"Количество созданных кубов за все время: {count}";
    }
}
