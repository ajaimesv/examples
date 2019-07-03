
```scala
import scala.swing._
import scala.swing.event._

object SwingApp extends SimpleSwingApplication {
    def top = new MainFrame {
        title = "First Swing App"
        val button = new Button {
            text = "Click me"
        }
        val label = new Label {
            text = "No clicks registered"
        }
        contents = new BoxPanel(Orientation.Vertical) {
            contents += button
            contents += label
            border = Swing.EmptyBorder(30, 30, 10, 30)
        }
        listenTo(button)
        var clicks = 0
        reactions += {
            case ButtonClicked(b) =>
                clicks += 1
                label.text = "Number of clicks: " + clicks
        }
    }
}
```
