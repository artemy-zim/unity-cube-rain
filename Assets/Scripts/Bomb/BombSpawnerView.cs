public class BombSpawnerView : SpawnerView<Bomb>
{
    protected override void UpdateActive(int count)
    {
        ActiveText.text = $"Количество активных бомб на сцене: {count}";
    }

    protected override void UpdateCreated(int count)
    {
        CreatedText.text = $"Количество созданных бомб за все время: {count}";
    }
}
