import React,{Component} from 'react';
import {Table} from 'react-bootstrap';

import {Button,ButtonToolbar} from 'react-bootstrap';
import {AddModal} from './AddModal';

export class Predict extends Component{

    constructor(props){
        super(props);
        this.state={predsales:[], addModalShow:false, editModalShow:false}
    }

    refreshList(){
        fetch(process.env.REACT_APP_API+'user')
        .then(response=>response.json())
        .then(data=>{
            this.setState({predsales:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }

    updateSales(saleid){
        fetch(process.env.REACT_APP_API+'user/'+saleid,{
            method:'DELETE',
            header:{'Accept':'application/json',
                    'Content-Type':'application/json'}
        })
    }

    render(){
        const {predsales: psales}=this.state;
        let addModalClose=()=>this.setState({addModalShow:false});
        return(
            <div >
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                        <th>SalesId</th>
                        <th>Date</th>
                        <th>Sale</th>
                        </tr>
                    </thead>
                    <tbody>
                        {psales.map(sale=>
                            <tr key={sale.SalesId}>
                                <td>{sale.SalesId}</td>
                                <td>{sale.Dos}</td>
                                <td>{sale.Sales}</td>
                            </tr>)}
                    </tbody>

                </Table>      
                <ButtonToolbar>
                    <Button variant='primary' onClick={()=>this.setState({addModalShow:true})}>Import</Button>
                    <Button variant='primary' onClick={()=>this.updateSales}>Predict</Button>
                    <AddModal show={this.state.addModalShow}
                    onHide={addModalClose}/>
                </ButtonToolbar>
            </div>
        )
    }
}